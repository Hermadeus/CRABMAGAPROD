using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public interface IBehaviour { }

    public interface IMovementBehaviour : IBehaviour
    {
        void Move(Entity _entity) ;
        
        void StopMove(Entity _entity);
    }

    public interface IDetectSomethingBehaviour : IBehaviour
    {
        void Detect(Unit _unit);
    }

    public interface IAttack
    {
        void Attack(Unit _unit, IAttackReceiver _target);
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


    /// <summary>
    /// Passif
    /// </summary>
    public interface IPassifBehaviour : IBehaviour
    {
        void PassifEffect(Unit unit);
    }

    public interface IStuntAttacker
    {
        float StuntTime { get; set; }
    }

    public interface IStuntable
    {
        void Stunt();
    }
}