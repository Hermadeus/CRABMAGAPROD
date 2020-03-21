using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class CrabFormation : MonoBehaviour, IPoolable, IPushable
    {
        public AP_GameManager AP_GameManager = default;
        public PoolingManager poolingManager = default;

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

        public void OnPool()
        {
        }

        public void OnPush()
        {
        }
    }
}