using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Attack/Simple Attack")]
    public class SimpleAttack : BaseAttack
    {
        public override void Attack(Unit _unit, IAttackReceiver _receiver)
        {
            base.Attack(_unit, _receiver);
        }

        public override IEnumerator AttackCor(Unit _unit, IAttackReceiver _receiver)
        {
            yield return new WaitForSeconds(_unit.AttackSpeed);

            if (_unit == null || _receiver == null)
                yield break;

            Effect(_unit, _receiver);

            if (_unit == null || _receiver == null)
                yield break;

            _unit.attackCor = _unit.StartCoroutine(AttackCor(_unit, _receiver));

            yield break;
        }

        public override void Effect(Unit _unit, IAttackReceiver _receiver)
        {
            base.Effect(_unit, _receiver);

            _receiver?.ReceiveAttack(_unit, _unit.Damage);
        }
    }
}