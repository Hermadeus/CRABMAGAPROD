using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Conditions/Test")]
    public class IA_ConditionTest : IA_Condition
    {
        public override bool Condition(IA_Manager manager)
        {
            if (manager.APgameManager.enemiesOnBattle.Count < 3)
                return true;

            return false;
        }
    }
}