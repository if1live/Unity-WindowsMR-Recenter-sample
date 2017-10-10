using UnityEngine;
using UnityEngine.XR;
#if UNITY_WSA
using UnityEngine.XR.WSA;
#endif

namespace Assets.Chiho {
    public class Recenter_WinMR {
        /*
         * https://docs.unity3d.com/ScriptReference/VR.InputTracking.Recenter.html
         */

        public static bool IsRequired() {
#if UNITY_WSA
            var loadedDev = XRSettings.loadedDeviceName;
            if(loadedDev != "WindowsMR") {
                return false;
            }
            // skip hololens. I don't need hololens now
            if(HolographicSettings.IsDisplayOpaque == false) {
                return false;
            }
            return true;
#else
            return false;
#endif
        }
        
        public static void Recenter(Transform tr_root, Transform tr_cam) {
            // rotate. use only y-axis rotation
            var cam_rot = tr_cam.localRotation;
            var e = cam_rot.eulerAngles;
            var rot = Quaternion.Euler(0, -e.y, 0);
            tr_root.localRotation = rot;

            // position
            var cam_pos = tr_cam.localPosition;
            // y, inverse
            Vector3 pos = new Vector3(0, -cam_pos.y, 0);

            // use xz-plane
            // root_pos + mat_root * cam_pos = Vector2.zero
            // root_pos = -(mat_root * cam_pos)
            var rad = e.y * Mathf.Deg2Rad;
            var cosval = Mathf.Cos(rad);
            var sinval = Mathf.Sin(rad);
            pos.x = -(cam_pos.x * cosval - cam_pos.z * sinval);
            pos.z = -(cam_pos.x * sinval + cam_pos.z * cosval);
            
            tr_root.localPosition = pos;
        }
    }
}
