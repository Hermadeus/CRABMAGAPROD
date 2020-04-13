using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Attack/Collider Attack")]
    public class AttackCollider : SimpleAttack
    {
        public override void Effect(Unit _unit, IAttackReceiver _receiver)
        {
            //base.Effect(_unit, _receiver);

            Enemy e = _unit as Enemy;

            for (int i = 0; i < e.hitColliders.Length; i++)
            {
                if (e.hitColliders[i].GetComponentInParent<Unit>() is IAttackReceiver)
                {
                    base.Effect(_unit, e.hitColliders[i].GetComponentInParent<Unit>() as IAttackReceiver);
                    Debug.Log(_unit.name + " ATTACK COLLIDER -> " + e.hitColliders[i].GetComponentInParent<Unit>().name);
                }
            }
        }
    }
}