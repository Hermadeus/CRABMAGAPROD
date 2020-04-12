using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/Random Instantiation")]
    public class IA_RandomInstantiation : IA_InstantationRules
    {
        public EnemyData[] enemyDatas = default;

        public IA_InstantiateTimer timerInstantiation = default;

        public override Entity Instantiation(IA_Manager manager)
        {
            int x = Random.Range(0, enemyDatas.Length);

            return manager.poolingManager.PoolEntity(enemyDatas[x].unitType.GetType(), manager.APgameManager.castle.transform.position);
        }
    }
}