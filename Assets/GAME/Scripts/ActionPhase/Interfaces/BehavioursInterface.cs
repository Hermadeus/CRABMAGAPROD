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
        void ReceiveAttack(Unit attaquant, float _damage);
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
        bool AsStunt { get; set; }
    }

    public interface IStuntable
    {
        void Stunt();
    }

    public interface IBoostSpeedAttackOnOther
    {
        float AttackSpeedBoostTimer { get; set; }
        float AttackSpeedMultiplier { get; set; }
    }

    public interface ILaserAttacker
    {
        float LaserDamage { get; set; }
        Transform StartPos { get; set; }
        Transform EndPos { get; set; }
        float LaserSize { get; set; }
        float LaserChargeTime { get; set; }

        LineRenderer LineRendererLaser { get; set; }
        Collider[] LaserTarget { get; set; }

        void ChargeLaser();
        void StartLaser();
        void StopLaser();
    }

    public interface IPastillable
    {
        void SetPastille();
    }

    public interface IAttire
    {

    }
}