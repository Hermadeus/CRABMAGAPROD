using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/4N")]

    public class IA_4N : IA_Behaviour
    {
        public IA_CounterEnemyOnLine Ia_CounterEnemyOnLine = default;
        public IA_RandomInstantiationOnLine IA_RandomInstantiationOnLine = default; 

        public override void CallEvent(IA_Manager manager)
        {
            base.CallEvent(manager);

            if(manager.APgameManager.crabFormationOnBattle.Count <= 2)
            {
                IA_RandomInstantiationOnLine.RandomInstantionOnLine(manager, Mathf.RoundToInt(manager.APgameManager.leaderOnBattle.transform.position.x));
            }

            if(manager.APgameManager.crabFormationOnBattle.Count >= 3)
            {
                manager.StartCoroutine(T(manager));
            }
        }

        IEnumerator T(IA_Manager manager)
        {
            Entity e = Ia_CounterEnemyOnLine.CounterInstantionOnLine(manager, Mathf.RoundToInt(manager.APgameManager.leaderOnBattle.transform.position.x));

            yield return new WaitForSeconds(2f);
            IA_RandomInstantiationOnLine.RandomInstantionOnLine(manager, Mathf.RoundToInt(manager.APgameManager.leaderOnBattle.transform.position.x));

            yield break;
        }
    }
}