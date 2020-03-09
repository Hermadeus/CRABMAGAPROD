using UnityEngine.Events;
using UnityEngine;

namespace QRTools.Events
{
    public class EventHook_Start : MonoBehaviour
    {
        public UnityEvent onAwake = new UnityEvent();

        private void Start()
        {
            onAwake.Invoke();
        }
    }
}