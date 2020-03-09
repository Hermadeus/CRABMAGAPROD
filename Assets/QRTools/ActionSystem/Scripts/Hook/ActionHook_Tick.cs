using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace QRTools.Actions
{
    public class ActionHook_Tick : ActionHookBase
    {
        [SerializeField] private float timer = 1f;
        public float Timer
        {
            get
            {
                return timer;
            }
            set
            {
                timer = value;
                wait = new WaitForSeconds(value);
            }
        }

        WaitForSeconds wait;

        private void Awake()
        {
            wait = new WaitForSeconds(timer);
        }

        private void Start()
        {
            StartCoroutine(Tick());
        }

        IEnumerator Tick()
        {
            if(actions != null)
                for (int i = 0; i < actions.Length; i++)
                {
                    actions[i].Execute();
                }
            yield return wait;
            StartCoroutine(Tick());
        }

        private void OnValidate()
        {
            wait = new WaitForSeconds(timer);
        }
    }
}
