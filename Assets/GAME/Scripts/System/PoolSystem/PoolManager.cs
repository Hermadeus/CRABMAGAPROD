using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class PoolManager : SerializedMonoBehaviour, IPoolBehaviour
    {
        [TableList(ShowPaging = true)]
        public List<Unit> unitsQueue = new List<Unit>();

        public Vector3 poolPosition = new Vector3(100, 0, 100);

        public AP_GameManager ap_GameManager;

        public Crab PoolCrabToUnit(CrabUnit crabUnit)
        {
            EntitiesInUnit entitieInUnit = crabUnit.crabFormation.GetNextEmptyPosition();
            Unit unit = Pool<Crab>(entitieInUnit.transformObject.position);
            Crab crab = unit as Crab;
            entitieInUnit.crab = crab;
            crab.transform.parent = crabUnit.crabsParent;
            return crab;
        }

        public List<Crab> PoolCrabsToUnit(CrabUnit crabUnit, int quantite)
        {
            List<Crab> crabs = new List<Crab>();

            for (int i = 0; i < quantite; i++)
            {
                Crab c = PoolCrabToUnit(crabUnit);
                crabs.Add(c);
                ap_GameManager.crabsInvoke.Add(c);
            }

            return crabs;
        }

        #region Pool Functions
        public Unit Pool<T>(Vector3 position) where T : Unit
        {
            Unit unit = FindUnit<T>();
            unitsQueue.Remove(unit);

            unit.transform.position = position;
            unit.IsPool = true;

            return unit;
        }
        #endregion
        
        Unit FindUnit<T>() where T : Unit
        {
            for (int i = 0; i < unitsQueue.Count; i++)
            {
                if (unitsQueue[i] is T && !unitsQueue[i].IsPool)
                {
                    return unitsQueue.ElementAt(i) as Unit;
                }
            }

            throw new Exception($"Impossible de pool cet objet car il n'y en a aucun de disponible");
        }

#if UNITY_EDITOR
        [Button()]
        void FindAllUnitsInScene()
        {
            unitsQueue.Clear();
            var unitArray = FindObjectsOfType<Unit>();
            for (int i = 0; i < unitArray.Length; i++)
            {
                unitsQueue.Add(unitArray[i]);
            }
        }
#endif
    }
}