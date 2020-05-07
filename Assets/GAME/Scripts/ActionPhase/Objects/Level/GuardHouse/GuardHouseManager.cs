using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Functions;

namespace CrabMaga
{
    public class GuardHouseManager : MonoBehaviour
    {
        public List<GuardHouse> guardHouses = new List<GuardHouse>();

        public PoolingManager poolingManager = default;

        public AP_GameManager AP_GameManager = default;

        public bool allEmpty = false;

        public Vector2Int linesMinMax = new Vector2Int();

        public int GetRandomLine() => Random.Range(linesMinMax.x, linesMinMax.y + 1);
        
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

        public GuardHouse GetNearnestGuardHouseOfThisPosition(Vector3 position)
        {
            float d = Mathf.Infinity;
            GuardHouse gh = null;

            for (int i = 0; i < guardHouses.Count; i++)
            {
                if (Vector3.Distance(guardHouses[i].transform.position, position) < d && !guardHouses[i].isOccupy)
                {
                    d = Vector3.Distance(guardHouses[i].transform.position, position);
                    gh = guardHouses[i];
                }
            }

            return gh;
        }

        public GuardHouse GetGuardHouseOnThisLine(int line)
        {
            for (int i = 0; i < guardHouses.Count; i++)
            {
                if (guardHouses[i].lineIndex == line)
                    return guardHouses[i];
            }

            return null;
        }

        public GuardHouse GetGuardHouseLineWithHighterUnits()
        {
            List<float> values = new List<float>();
            CrabFormation crabFormation = AP_GameManager.GetFormationWithHighterCrabs();
            if (crabFormation == null)
                return null;

            for (int i = 0; i < crabFormation.CrabUnits.Count; i++)
                if (crabFormation.CrabUnits[i] != null)
                    values.Add(crabFormation.CrabUnits[i].transform.position.x);

            int averragePosX = Mathf.RoundToInt(FunctionsUseful.GetAverrage(values.ToArray()));

            return GetGuardHouseOnThisLine(averragePosX);
        }
    }
}