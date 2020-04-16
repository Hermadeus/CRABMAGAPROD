using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    public class BaseAttack : Behaviour, IAttackBehaviour
    {
        public virtual void Attack(Unit _unit, IAttackReceiver _receiver)
        {
            if (_unit.attackCor != null)
                _unit.StopCoroutine(_unit.attackCor);

            //_unit.transform.DOLookAt(((Entity)_receiver).transform.position, .5f);

            _unit.attackCor = _unit.StartCoroutine(AttackCor(_unit, _receiver));
        }

        public virtual IEnumerator AttackCor(Unit _unit, IAttackReceiver _receiver)
        {
            yield break;
        }

        public virtual void Effect(Unit _unit, IAttackReceiver _receiver)
        {
            Debug.Log(_unit.name +  " ATTACK -> " + ((Entity)_receiver).name);

            _unit.transform.DOLookAt(((Entity)_receiver).transform.position, .5f);

            if(_unit.animator != null)
                _unit.animator?.SetTrigger("onAttack");

            SoundManager.instance.PlaySound(_unit.attackSound, _unit.audiosource);
        }
    }
}