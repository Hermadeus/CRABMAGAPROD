using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/Random Counter ")]

    public class IA_CounterEnemy : IA_InstantationRules
    {
        public IA_CounterMatrice matrice = default;

        public override Entity Instantiation(IA_Manager manager)
        {
            return manager.poolingManager.PoolEntity(
                matrice.GetCounter(manager.poolingManager.latestUnitInstiate.entityData).unitType.GetType(),
                manager.APgameManager.castle.transform.position,
                manager.poolingManager.poolingParent
                );
        }
    }
}