using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public interface IBehaviour { }

    public interface IMovementBehaviour : IBehaviour
    {
        void Move(Entity _entity);
        
        void StopMove(Entity _entity);
    }

    public interface IDetectSomethingBehaviour : IBehaviour
    {
        void Detect(Unit _unit);
    }

    public interface IAttackBehaviour : IBehaviour
    {
        void Attack(Unit _unit, IAttackReceiver _receiver);
        IEnumerator AttackCor(Unit _unit, IAttackReceiver _receiver);
    }

    public interface IAttackReceiver : IBehaviour
    {
        void ReceiveAttack(float _damage);
    }

    public interface IPassifBehaviour : IBehaviour
    {

    }
}