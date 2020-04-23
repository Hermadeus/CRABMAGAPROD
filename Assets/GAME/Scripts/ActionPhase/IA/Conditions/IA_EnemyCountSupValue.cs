using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Conditions/Enemy Sup Value")]
    public class IA_EnemyCountSupValue : IA_Condition
    {
        public float value = 2f;

        public override bool Condition(IA_Manager manager)
        {
            if(manager.APgameManager.enemiesOnBattle.Count <= value)
            {
                return true;
            }

            return false;
        }
    }
}