using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.CameraSystem
{
    public class OnEnable_AssignCameraVariable : MonoBehaviour
    {
        public CameraVariable cameraVariable = default;

        private void OnEnable()
        {
            cameraVariable.value = GetComponent<Camera>();
            Destroy(this);
        }
    }
}