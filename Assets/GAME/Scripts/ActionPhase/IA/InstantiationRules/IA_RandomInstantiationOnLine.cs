using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/Random Instantiation On Line")]

    public class IA_RandomInstantiationOnLine : IA_InstantationRules
    {
        public EnemyData[] enemyDatas = default;

        public override Entity Instantiation(IA_Manager manager)
        {
            int x = Random.Range(0, enemyDatas.Length);

            Entity e = manager.poolingManager.PoolEntity(
                enemyDatas[x].unitType.GetType(),
                new Vector3(
                    manager.APgameManager.castle.transform.position.x,
                    manager.APgameManager.castle.transform.position.y,
                    manager.APgameManager.castle.transform.position.z)
                );

            if (e.gameManager.guardHouseManager.GetGuardHouseLineWithHighterUnits() != null)
            {
                e.Destination = e.gameManager.guardHouseManager.GetGuardHouseLineWithHighterUnits();
                e.transform.position = new Vector3(e.Destination.transform.position.x, e.transform.position.y, e.transform.position.z);
            }

            return e;
        }
    }
}