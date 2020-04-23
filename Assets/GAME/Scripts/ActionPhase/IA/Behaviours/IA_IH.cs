using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/IH")]
    public class IA_IH : IA_Behaviour
    {
        public IA_EnemyOfThisLine IA_EnemyOfThisLine = default;

        public override void CallEvent(IA_Manager manager)
        {
            if (IA_EnemyOfThisLine.SpecialCond(manager, Mathf.RoundToInt(manager.poolingManager.latestUnitInstiate.transform.position.x)))
            {
                base.CallEvent(manager);
                instantiationRule.Instantiation(manager);
            }
        }
    }
}