using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{

    //COMPORTEMENT FOR ENTITIES
    
    public interface IMovementBehaviour
    {
        float BaseSpeed { get; set; }
        void Move(Unit unit);

        void MoveTowardTarget(Unit unit, Transform target, Action arrivedAction);

        void StopMove(Unit unit);

        void RestartMove(Unit unit);
    }

    public interface IAttackBehaviour
    {
        void Attack(Unit unit);
    }

    public interface IDieBehaviour
    {
        void Die(Unit unit);
    }

    public interface ISpecialAttackBehaviour
    {
        IAttackBehaviour AttackBehaviour { get; set; }
    }

    public interface IUnitFormationBehaviour
    {
        int MaxEntitiesInUnit { get; set; }

        List<Vector3> EntitiesPositionsOffset { get; set; }

        FormationShape FormationShape { get; set; }
    }

    public interface IPoolBehaviour
    {
        /// <summary>
        /// Pull an objest to a position
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <returns></returns>
        Unit Pool<T>(Vector3 position) where T : Unit;
    }

    public interface IPoolable
    {
        bool IsPool { get; set; }

        /// <summary>
        /// Use Push to put the object in the queue.
        /// </summary>
        void Push();
    }

    public interface IDetectBehaviour
    {
        Collider[] DetectEnemies(Unit unit);

        Unit GetNearestUnitToAttack(Unit unit);
    }

    public interface ISurroundBehaviour
    {
        void Surround(Unit unit, Transform target);
    }
}
