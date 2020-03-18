using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    public static class MovementFunctions
    {
        public static void GoTo(Entity _entity, Vector3 destinationPoint, bool rotationAndMove = false)
        {
            if (!rotationAndMove)
            {
                _entity.transform.DOLookAt(destinationPoint, _entity.rotationSpeed);
                _entity.movementTween = _entity.transform.DOMove(destinationPoint, GetTimeMovement(_entity, destinationPoint));
            }
            else
            {
                _entity.transform.DOLookAt(destinationPoint, _entity.rotationSpeed).OnComplete(delegate {
                    _entity.movementTween = _entity.transform.DOMove(destinationPoint, GetTimeMovement(_entity, destinationPoint));
                });
            }
        }

        public static float GetTimeMovement(Entity _entity, Vector3 _pointToReach)
        {
            float t = 0f;
            t = Vector3.Distance(_pointToReach, _entity.position);
            t /= _entity.entityData.baseSpeed;
            return t;
        }
    }
}