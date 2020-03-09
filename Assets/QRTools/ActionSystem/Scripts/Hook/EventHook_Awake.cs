using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QRTools.Events
{
    public class EventHook_Awake : MonoBehaviour
    {
        public UnityEvent onAwake = new UnityEvent();

        private void Awake()
        {
            onAwake.Invoke();
        }
    }
}
