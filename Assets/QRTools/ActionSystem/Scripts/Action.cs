using UnityEngine;

namespace QRTools.Actions
{
    public abstract class Action : ScriptableObject
    {
        [TextArea(3,5)]
        [SerializeField] private string description;

        public abstract void Execute();
    }
}
