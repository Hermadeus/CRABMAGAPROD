using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Movement/SimpleMovement")]
    public class SimpleMovement : Movement
    {
        /// <summary>
        /// The unit go all right.
        /// </summary>
        /// <param name="unit"></param>
        public override void Move(Unit unit)
        {
            unit.transform.position += unit.transform.forward * unit.Speed * deltaTime.Value;
        }

        /// <summary>
        /// Reset the unit to his current Speed.
        /// </summary>
        /// <param name="unit"></param>
        public override void RestartMove(Unit unit)
        {
            DOTween.To(() => unit.Speed, (x) => unit.Speed = x, BaseSpeed, easingTime).SetEase(ease).OnComplete(delegate { RestartMoving(unit); });            
        }

        /// <summary>
        /// Set the speed To 0.
        /// </summary>
        /// <param name="unit"></param>
        public override void StopMove(Unit unit)
        {
            DOTween.To(() => unit.Speed, (x) => unit.Speed = x, 0, easingTime).SetEase(ease).OnComplete(delegate { RestartMoving(unit); });
        }
    }
}