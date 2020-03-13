using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public abstract class BaseAttack : ScriptableObject, IAttackBehaviour
    {
        public virtual void Attack(Unit unit)
        {
            unit.StartCoroutine(AttackCor(unit));
        }

        public virtual IEnumerator AttackCor(Unit unit)
        { 
            if(Vector3.Distance(unit.transform.position, unit.UnitTarget.transform.position) <= unit.unitData.attackRange)
                AttackEffect(unit);

            yield return new WaitForSeconds(unit.unitData.attackSpeed);

            if (unit.UnitTarget != null)
                unit.StartCoroutine(AttackCor(unit));
            else
                unit.asSurround = false;

            yield break;
        }

        public virtual void AttackEffect(Unit unit)
        {
            if (unit.UnitTarget == null)
            {
                unit.asSurround = false;
                return;
            }

            unit.UnitTarget.GetDamage(unit.Damage);
        }
    }
}