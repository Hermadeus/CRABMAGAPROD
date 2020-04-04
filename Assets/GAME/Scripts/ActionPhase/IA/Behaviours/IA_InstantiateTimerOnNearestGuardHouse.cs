using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/Instantiate with Timer On Nearest Guard House")]
    public class IA_InstantiateTimerOnNearestGuardHouse : IA_Behaviour
    {
        public float timer = 5f;

        WaitForSeconds wait;

        public override void CallEvent(IA_Manager manager)
        {
            wait = new WaitForSeconds(timer);

            manager.StartCoroutine(TimerCor(manager));
        }

        public IEnumerator TimerCor(IA_Manager manager)
        {
            if (manager.guardHouseManager.allEmpty)
            {
                yield return wait;
                manager.StartCoroutine(TimerCor(manager));
                yield break;
            }

            if (manager.APgameManager.crabFormationOnBattle.Count == 0)
            {
                yield return wait;
                manager.StartCoroutine(TimerCor(manager));
                yield break;
            }

            instantiationRule.Instantiation(manager);

            yield return wait;

            manager.StartCoroutine(TimerCor(manager));

            yield break;
        }
    }
}