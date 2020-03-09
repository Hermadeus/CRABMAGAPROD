using UnityEngine;

namespace QRTools.Variables
{
    public abstract class EnumVariable : ScriptableObject
    {
        [SerializeField] [TextArea(3,5)] private string description;

        [Tooltip("check startValue to init this value a the start of the game")]
        public bool affectStartValue = true;
    }
}
