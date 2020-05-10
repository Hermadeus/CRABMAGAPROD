using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Stars conditions/Unit Star Condition")]

    public class UnitCondition : StarWinCondition
    {
        public int unitToReach = 8;

        public override bool WinStar(AP_GameManager gm)
        {
            if (gm.levelData.maxCrab - gm.crabUnitOnBattle.Count < unitToReach)
                return true;

            return false;
        }
    }
}