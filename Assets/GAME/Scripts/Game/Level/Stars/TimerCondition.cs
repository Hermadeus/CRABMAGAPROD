using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Stars conditions/Timer Star Condition")]

    public class TimerCondition : StarWinCondition
    {
        public float timeToReach = 50;

        public override bool WinStar(AP_GameManager gm)
        {
            if (gm.AP_Timer < timeToReach)
                return true;

            return false;
        }
    }
}