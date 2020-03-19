using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Movement/Follow Target Movement")]
    public class FollowTargetMovement : BaseMovement
    {
        public override void Move(Entity _entity)
        {
            base.Move(_entity);

            if (_entity is Unit) {
                Unit _unit = _entity as Unit;
                                
                _entity.transform.DOLookAt(_unit.Target.position, _entity.rotationSpeed);
                _entity.movementTween = _entity.transform.DOMove(_unit.Target.position, MovementFunctions.GetTimeMovement(_entity, _unit.Target.position));
                // A CORRIGER
            }
        }
    }
}