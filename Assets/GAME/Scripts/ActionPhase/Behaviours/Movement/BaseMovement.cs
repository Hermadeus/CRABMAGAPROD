using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    public class BaseMovement : Behaviour, IMovementBehaviour
    {
        public virtual void Move(Entity _entity)
        {
            _entity.movementTween.Kill();

            if (_entity.Speed == 0)
                DOTween.To(() => _entity.Speed, x => _entity.Speed = x, _entity.entityData.baseSpeed, _entity.entityData.acceleration);
        }

        public void StopMove(Entity _entity)
        {
            DOTween.To(() => _entity.Speed, x => _entity.Speed = x, 0, .1f);
        }
    }
}