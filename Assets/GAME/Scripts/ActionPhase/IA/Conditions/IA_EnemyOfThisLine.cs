using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Conditions/Enemy on this line")]
    public class IA_EnemyOfThisLine : IA_Condition
    {
        public override bool Condition(IA_Manager manager)
        {
            return true;
        }

        public bool SpecialCond(IA_Manager manager, int line)
        {
            for (int i = 0; i < manager.APgameManager.enemiesOnBattle.Count; i++)
            {
                if (Mathf.RoundToInt(manager.APgameManager.enemiesOnBattle[i].transform.position.x) == line)
                {
                    return false;
                }
            }

            return true;
        }
    }
}