using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Passif/Double Effectif Effect")]
    public class DoubleEffectifEffect : BasePassifEffect
    {
        public override void PassifEffect(Unit unit)
        {
            CrabUnit crabUnit = unit as CrabUnit;

            if (crabUnit == null || crabUnit.crabFormationReference == null) return;

            //Debug.Log("DoubleEffectif" + crabUnit.crabFormationReference.name);

            for (int i = 0; i < crabUnit.crabFormationReference.CrabUnits.Count; i++)
            {
                if (crabUnit.crabFormationReference.CrabUnits[i] != null)
                {
                    crabUnit.poolingManager.PoolEntity(
                        crabUnit.entityData.unitType.GetType(),
                        crabUnit.crabFormationReference.CrabUnits[i].transform.position
                        );
                }
            }
        }
    }
}