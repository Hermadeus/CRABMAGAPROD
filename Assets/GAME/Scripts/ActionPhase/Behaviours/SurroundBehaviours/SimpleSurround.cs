using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Surround/Simple Surround")]
    public class SimpleSurround : ScriptableObject, ISurroundBehaviour
    {
        public void Surround(Unit unit, Transform target)
        {
            CrabUnit crabUnit = unit as CrabUnit;

            unit.asSurround = true;

            for (int i = 0; i < crabUnit.crabFormation.entities.Count; i++)
            {
                if(crabUnit.crabFormation.entities[i].crab != null)
                {
                    crabUnit.crabFormation.entities[i].crab.transform.position += new Vector3(Random.insideUnitSphere.x * .1f, 0, Random.insideUnitSphere.z * .1f);
                }
            }

            unit.HaveReachEnemy();
        }
    }
}