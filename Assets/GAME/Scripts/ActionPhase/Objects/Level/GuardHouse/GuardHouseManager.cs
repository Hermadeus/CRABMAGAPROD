using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class GuardHouseManager : MonoBehaviour
    {
        public List<GuardHouse> guardHouses = new List<GuardHouse>();

        public PoolingManager poolingManager = default;

        public EnemyData enemyData = default;

        public bool allEmpty = false;

        private void Awake()
        {
            //poolingManager.PoolEntity(enemyData.unitType.GetType(), Vector3.zero);
        }

        public GuardHouse GetNextEmptyGuardHouse()
        {
            for (int i = 0; i < guardHouses.Count; i++)
            {
                if (!guardHouses[i].isOccupy)
                {
                    guardHouses[i].isOccupy = true;
                    return guardHouses[i];
                }
            }
            return null;
        }

        [Button]
        public void FindAllGuardHouse()
        {
            guardHouses.Clear();
            var g = FindObjectsOfType<GuardHouse>();

            for (int i = 0; i < g.Length; i++)
            {
                guardHouses.Add(g[i]);
            }

            Debug.Log("GUARDHOUSES : " + guardHouses.Count + " are founded.");
        }
    }
}