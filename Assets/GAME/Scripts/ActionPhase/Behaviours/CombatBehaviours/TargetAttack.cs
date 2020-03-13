using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Combat/Target Attack")]
    public class TargetAttack : BaseAttack
    {
        public override void AttackEffect(Unit unit)
        {
            base.AttackEffect(unit);

            unit.StartCoroutine(AttCor(unit));            
        }

        IEnumerator AttCor(Unit unit)
        {
            CrabUnit crabUnit = unit.UnitTarget as CrabUnit;

            for (int i = 0; i < unit.Damage; i++)
            {
                crabUnit.crabFormation.PushRandomCrab();
                yield return new WaitForEndOfFrame(); 
            }

            yield break;
        }
    }
}