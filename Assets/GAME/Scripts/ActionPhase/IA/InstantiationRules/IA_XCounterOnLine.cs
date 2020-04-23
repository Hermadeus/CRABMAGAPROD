using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/X Instantiation On Line")]

    public class IA_XCounterOnLine : IA_InstantationRules
    {
        public IA_CounterEnemyOnLine iA_CounterEnemyOnLine = default;
        public IA_CounterMatrice IA_CounterMatrice = default;

        public int nbrUnitToInvoke = 2;

        public float timeBetweenEachInstantiation = 2f;

        public override Entity Instantiation(IA_Manager manager)
        {
            manager.StartCoroutine(T(manager));

            return null;
        }

        IEnumerator T(IA_Manager manager)
        {
            EntityData[] ed = IA_CounterMatrice.GetRandomCounter(manager.poolingManager.latestUnitInstiate.entityData, nbrUnitToInvoke);

            for (int i = 0; i < nbrUnitToInvoke; i++)
            {
                //iA_CounterEnemyOnLine.Instantiation(manager);

                //Debug.Log(i + "  " + nbrUnitToInvoke);

                Enemy e = manager.poolingManager.PoolEntity(
                ed[i].unitType.GetType(),
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

                yield return new WaitForSeconds(timeBetweenEachInstantiation);
            }            

            yield break;
        }
    }
}