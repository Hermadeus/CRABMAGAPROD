using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/2N")]

    public class IA_2N : IA_Behaviour
    {
        public IA_EnemyOfThisLine IA_EnemyOfThisLine = default;

        public override void CallEvent(IA_Manager manager)
        {
            base.CallEvent(manager);
            
            if(IA_EnemyOfThisLine.SpecialCond(manager, Mathf.RoundToInt(manager.poolingManager.latestUnitInstiate.transform.position.x)))
            {
                instantiationRule.Instantiation(manager);
            }
        }
    }
}