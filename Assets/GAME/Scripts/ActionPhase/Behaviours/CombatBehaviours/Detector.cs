using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Combat/Detector")]
    public class Detector : ScriptableObject, IDetectBehaviour
    {
        public Collider[] DetectEnemies(Unit unit)
        {
           return Physics.OverlapSphere(unit.transform.position, unit.detectableRange, unit.layerMaskToDetect);
        }

        public Unit GetNearestUnitToAttack(Unit unit)
        {
            Unit nearestUnit = null;
            float d = 1000;

            for (int i = 0; i < unit.UnitDetectable.Length; i++)
            {
                float d1 = Vector3.Distance(unit.transform.position, unit.UnitDetectable[i].transform.position);
                if (d1 < d)
                {
                    d = d1;
                    nearestUnit = unit.UnitDetectable[i].GetComponentInParent<Unit>();
                }
            }

            return nearestUnit;
        }
    }
}