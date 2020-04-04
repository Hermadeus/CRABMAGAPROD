using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/Instantiate with Timer")]
    public class IA_InstantiateTimer : IA_Behaviour
    {
        public float timer = 5f;

        public override void CallEvent(IA_Manager manager)
        {
            manager.StartCoroutine(TimerCor(manager));
        }

        public IEnumerator TimerCor(IA_Manager manager)
        {
            if (manager.guardHouseManager.allEmpty)
                yield break;

            instantiationRule.Instantiation(manager);

            yield return new WaitForSeconds(timer);

            manager.StartCoroutine(TimerCor(manager));

            yield break;
        }
    }
}