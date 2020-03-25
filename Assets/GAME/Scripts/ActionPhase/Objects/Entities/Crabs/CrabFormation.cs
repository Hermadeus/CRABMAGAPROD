using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using QRTools.Utilities;

namespace CrabMaga
{
    public class CrabFormation : MonoBehaviour, IResetable, IPoolable, IPushable
    {
        public AP_GameManager AP_GameManager = default;
        public PoolingManager poolingManager = default;

        public UnityEvent onInitEvent = new UnityEvent();

        [SerializeField] private List<CrabUnit> crabUnits = new List<CrabUnit>();
        public List<CrabUnit> CrabUnits
        {
            get => crabUnits;
            set
            {
                crabUnits = value;
                if (value.Count == 0)
                    poolingManager.Push(this);
            }
        }

        public bool haveReceivePassif = false;


        public void OnPool()
        {
            Init();
        }

        public void OnPush()
        {
            ResetObject();
        }

        public void Init()
        {
            onInitEvent?.Invoke();
        }

        public void ResetObject()
        {
            onInitEvent.RemoveAllListeners();
            crabUnits.Clear();
            haveReceivePassif = false;
        }
    }
}