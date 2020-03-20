using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class BaseAttack : Behaviour, IAttackBehaviour
    {
        public virtual void Attack(Unit _unit, IAttackReceiver _receiver)
        {
            _unit.attackCor = _unit.StartCoroutine(AttackCor(_unit, _receiver));
        }

        public virtual IEnumerator AttackCor(Unit _unit, IAttackReceiver _receiver)
        {
            yield break;
        }
    }
}