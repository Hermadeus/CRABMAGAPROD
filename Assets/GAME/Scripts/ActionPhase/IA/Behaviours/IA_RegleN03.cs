using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/Regle N3")]
    public class IA_RegleN03 : IA_Behaviour
    {
        public override void CallEvent(IA_Manager manager)
        {
            if (manager.APgameManager.crabUnitOnBattle.Count - manager.APgameManager.enemiesOnBattle.Count == 1)
            {
                Enemy e1 = instantiationRule.Instantiation(manager) as Enemy;
                Enemy e2 = instantiationRule.Instantiation(manager) as Enemy;

                e1.Destination = manager.guardHouseManager.GetGuardHouseLineWithHighterUnits();               
                e2.Destination = manager.guardHouseManager.GetGuardHouseOnThisLine(1);
            }
            else if (manager.APgameManager.crabUnitOnBattle.Count - manager.APgameManager.enemiesOnBattle.Count > 1)
            {
                Enemy e1 = instantiationRule.Instantiation(manager) as Enemy;
                e1.Destination = manager.guardHouseManager.GetGuardHouseLineWithHighterUnits();
            }
        }
    }
}