using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace Assets.ChihoExamples {
    class NativeRecenterExample : MonoBehaviour {
        const KeyCode key_recenter = KeyCode.R;

        void Start() {
            var ok = XRDevice.SetTrackingSpaceType(UnityEngine.XR.TrackingSpaceType.Stationary);
            Debug.LogFormat("TrackingSpace : {0}, {1}", XRDevice.GetTrackingSpaceType(), ok);
        }

        private void Update() {
            if (Input.GetKeyDown(key_recenter)) {
                Recenter();   
            }
        }

        internal void Recenter() {
            InputTracking.Recenter();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(NativeRecenterExample))]
    class NativeRecenterExampleEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var script = target as NativeRecenterExample;
            if (GUILayout.Button("Recenter")) {
                script.Recenter();
            }
        }
    }
#endif
}
