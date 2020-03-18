using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class GuardHouseManager : MonoBehaviour
    {
        public List<GuardHouse> guardHouses = new List<GuardHouse>();

        public GuardHouse GetNotOccupyGuardHouse()
        {
            for (int i = 0; i < guardHouses.Count; i++)
                if (guardHouses[i].IsOccupy == false)
                    return guardHouses[i];

            return null;
        }

        [Button()]
        void FindAllGuardHouses()
        {
            guardHouses.Clear();

            var guardHousesArray = FindObjectsOfType<GuardHouse>();
            for (int i = 0; i < guardHousesArray.Length; i++)
                guardHouses.Add(guardHousesArray[i]);
        }
    }
}