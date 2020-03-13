using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using DG.Tweening;

using QRTools.Variables;
using System;

namespace CrabMaga
{
    public abstract class Movement : SerializedScriptableObject, IMovementBehaviour
    {
        [SerializeField] protected FloatVariable deltaTime = default;

        [SerializeField, BoxGroup("Properties")] float baseSpeed = .5f;
        public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }

        [SerializeField, BoxGroup("Properties")] float baseRotationSpeed = .5f;
        public float BaseRotationSpeed { get => baseRotationSpeed; set => baseRotationSpeed = value; }

        [BoxGroup("Properties")]
        public Ease ease = Ease.Linear;
        [BoxGroup("Properties")]
        public float easingTime = 1f;

        public abstract void Move(Unit unit);

        /// <summary>
        /// Reset unit to his move
        /// </summary>
        /// <param name="unit"></param>
        public abstract void RestartMove(Unit unit);

        /// <summary>
        /// Set speed to with 0.
        /// </summary>
        /// <param name="unit"></param>
        public abstract void StopMove(Unit unit);

        protected void RestartMoving(Unit unit)
        {
            unit.IsMoving = true;
        }

        protected void StopMoving(Unit unit)
        {
            unit.IsMoving = false;
        }

        protected void GoAllRight(Unit unit)
        {
            unit.transform.position += unit.transform.forward * unit.Speed * deltaTime.Value;
        }

        public virtual void MoveTowardTarget(Unit unit, Transform target, Action arrivedAction)
        {
            if (target == null)
                return;

            
            if (Vector3.Distance(unit.transform.position, target.position) > .5f)
            {
                GoAllRight(unit);
                unit.transform.DOLookAt(unit.UnitTarget.transform.position, BaseRotationSpeed).SetEase(ease);
            }
            else if (Vector3.Distance(unit.UnitTarget.transform.position, unit.transform.position) < .5f)
            {
                arrivedAction.Invoke();
            }
        }
    }
}