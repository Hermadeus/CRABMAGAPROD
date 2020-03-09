using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.CameraSystem{
    [CreateAssetMenu(menuName = "QRTools/CameraSystem/CameraVariable")]
    public class CameraVariable : ScriptableObject
    {
        public Camera value { get; set; }
    }
}