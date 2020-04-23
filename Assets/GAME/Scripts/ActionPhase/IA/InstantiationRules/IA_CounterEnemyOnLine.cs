using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/Counter Instantiation On Line")]
    public class IA_CounterEnemyOnLine : IA_InstantationRules
    {
        public IA_CounterMatrice matrice = default;

        public override Entity Instantiation(IA_Manager manager)
        {
            Enemy e = manager.poolingManager.PoolEntity(
                matrice.GetBestCounter(manager.poolingManager.latestUnitInstiate.entityData).unitType.GetType(),
                manager.APgameManager.castle.transform.position,
                manager.poolingManager.poolingParent
                ) as Enemy;

            if (e.gameManager.guardHouseManager.GetGuardHouseLineWithHighterUnits() != null)
            {
                GuardHouse gh = e.gameManager.guardHouseManager.GetGuardHouseOnThisLine(Mathf.RoundToInt(manager.poolingManager.latestUnitInstiate.transform.position.x));

                if (gh != null)
                    e.Destination = gh;
                else
                    e.Destination = e.gameManager.guardHouseManager.GetNextEmptyGuardHouse();

                e.transform.position = new Vector3(e.Destination.transform.position.x, e.transform.position.y, e.transform.position.z);
            }

            return e;
        }

        public Entity CounterInstantionOnLine(IA_Manager manager, int line)
        {
            Enemy e = manager.poolingManager.PoolEntity(
                matrice.GetBestCounter(manager.poolingManager.latestUnitInstiate.entityData).unitType.GetType(),
                manager.APgameManager.castle.transform.position,
                manager.poolingManager.poolingParent
                ) as Enemy;

            GuardHouse gh = e.gameManager.guardHouseManager.GetGuardHouseOnThisLine(line);
                       
            e.transform.position = new Vector3(line, e.transform.position.y, e.transform.position.z);

            if (gh != null)
            {
                e.Destination = gh;
            }
            else
                e.Destination = e.gameManager.guardHouseManager.GetNextEmptyGuardHouse();

            return e;
        }
    }
}