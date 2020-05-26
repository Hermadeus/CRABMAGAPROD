using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/X Instantiation")]
    public class IA_XInstantiation : IA_InstantationRules
    {
        public EnemyData[] enemyDatas = default;

        public int nbr = 2;
        public float timer = .3f;

        Entity ent;

        public override Entity Instantiation(IA_Manager manager)
        {
            manager.StartCoroutine(Reinvoke(manager));

            return ent;
        }

        IEnumerator Reinvoke(IA_Manager manager)
        {
            for (int i = 0; i < nbr; i++)
            {
                int x = Random.Range(0, enemyDatas.Length);

                Entity e = manager.poolingManager.PoolEntity(
                    enemyDatas[x].unitType.GetType(),
                    new Vector3(
                        manager.APgameManager.castle.transform.position.x,
                        manager.APgameManager.castle.transform.position.y,
                        manager.APgameManager.castle.transform.position.z)
                    );

                ent = e;

                if (e.gameManager.guardHouseManager.GetGuardHouseLineWithHighterUnits() != null)
                {
                    e.Destination = e.gameManager.guardHouseManager.GetGuardHouseLineWithHighterUnits();
                    e.transform.position = new Vector3(e.Destination.transform.position.x, e.transform.position.y, e.transform.position.z);
                }

                yield return new WaitForSeconds(timer);
            }

            yield break;
        }
    }
}