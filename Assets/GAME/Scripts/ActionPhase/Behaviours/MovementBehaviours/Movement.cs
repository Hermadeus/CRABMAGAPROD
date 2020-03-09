using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using DG.Tweening;

using QRTools.Variables;

namespace CrabMaga
{
    public abstract class Movement : SerializedScriptableObject, IMovementBehaviour
    {
        [SerializeField] protected FloatVariable deltaTime = default;

        [SerializeField, BoxGroup("Properties")] float baseSpeed = .5f;
        public float BaseSpeed { get => baseSpeed; set => baseSpeed = value; }

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
    }
}