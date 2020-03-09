using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHookBase : MonoBehaviour
    {
        [TextArea(3,5)]
        [SerializeField] private string description;

        public Action[] actions;
    }
}
