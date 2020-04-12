using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Conditions/UnitCount Sup EnemyCount")]
    public class IA_Regle03Condition : IA_Condition
    {
        public override bool Condition(IA_Manager manager)
        {
            if (manager.APgameManager.crabUnitOnBattle.Count - manager.APgameManager.enemiesOnBattle.Count > 0)
                return true;

            return false;
        }
    }
}
