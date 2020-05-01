using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Stars conditions/Have Win Condition")]

    public class HaveWinCondition : StarWinCondition
    {
        public override bool WinStar(AP_GameManager gm)
        {
            if (gm.AsWin)
                return true;

            return false;
        }
    }
}