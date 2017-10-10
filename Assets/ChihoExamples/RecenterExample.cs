using Assets.Chiho;
using UnityEngine;
using UnityEngine.XR;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.ChihoExamples {
    class RecenterExample : MonoBehaviour {
        public Transform tr_root = null;
        public Transform tr_cam = null;

        const KeyCode key_recenter = KeyCode.R;

        private void Start() {
            // InputTracking.Recenter()
            // This only works with seated and standing experiences. Room scale experiences are not effected by Recenter.
            var ok = XRDevice.SetTrackingSpaceType(UnityEngine.XR.TrackingSpaceType.Stationary);
            Debug.LogFormat("TrackingSpace : {0}, {1}", XRDevice.GetTrackingSpaceType(), ok);
        }

        private void Update() {
            if(Input.GetKeyDown(key_recenter)) {
                ExecuteRecenter();   
            }
        }

        internal void ExecuteRecenter() {
            if (!Recenter_WinMR.IsRequired()) {
                return;
            }

            Recenter_WinMR.Recenter(tr_root, tr_cam);
            Debug.LogFormat("Recenter : {0}", Time.time);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RecenterExample))]
    class RecenterExampleEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var script = target as RecenterExample;
            if(GUILayout.Button("Recenter")) {
                script.ExecuteRecenter();
            }
        }
    }

#endif
}
