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
            }
        }

        public bool haveReceivePassif = false;

        public void TestDeath()
        {
            if (crabUnits.Count == 0)
                poolingManager.Push(this);
        }

        public void OnPool()
        {
            Init();
        }

        public void OnPush()
        {
            Debug.Log("formation death");
            AP_GameManager.crabFormationOnBattle.Remove(this);
            ResetObject();
        }

        public void Init()
        {
            onInitEvent?.Invoke();


            count = crabUnits.Count;
        }

        int count;

        public void ResetObject()
        {
            onInitEvent.RemoveAllListeners();
            crabUnits.Clear();
            haveReceivePassif = false;

            if(r != null)
                StopCoroutine(r);
            r = null;
        }

        Coroutine r = null;
        public Vector3 lastDeathPos = new Vector3();

        public void Ressucite(float timer)
        {
            if (r != null)
                StartCoroutine(RessuciteCor(timer));
        }

        public IEnumerator RessuciteCor(float timer)
        {
            yield return new WaitForSeconds(timer);

            if(crabUnits.Count == count)
                StartCoroutine(RessuciteCor(timer));

            if (crabUnits.Count <= 0)
                yield break;

            poolingManager.Pool(lastDeathPos, crabUnits[0].entityData.unitType.GetType());
            Debug.Log("ressucite");
            StartCoroutine(RessuciteCor(timer));
        } 
    }
}