using System.Collections;
using UnityEngine;

using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    public abstract class InputAction : SerializedScriptableObject
    {
        [TextArea(3, 5)]
        [SerializeField] string description = default;
        public bool isActive = true;
        [Tooltip("Input name (see Unity Input Manager)")]
        public string inputName;

        public abstract void Init();
        public abstract void Execute();

        public void SetActive(bool state) => isActive = state;
    }
}
