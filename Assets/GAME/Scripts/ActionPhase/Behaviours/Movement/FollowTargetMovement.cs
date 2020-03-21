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

            _entity.StartCoroutine(DistanceMinus(_entity, .5f));

        }

        IEnumerator DistanceMinus(Entity _entity, float distanceToCheck)
        {
            if (_entity is Unit)
            {
                Unit _unit = _entity as Unit;

                float d = Vector3.Distance(_entity.position, _unit.Target.position);

                while (d > distanceToCheck)
                {
                    if (((Unit)_unit.Target).AttackedBy == null)
                        yield break;

                    d = Vector3.Distance(_entity.position, _unit.Target.position);
                    _entity.transform.DOLookAt(_unit.Target.position, _entity.rotationSpeed);
                    _entity.movementTween = _entity.transform.DOMove(_unit.Target.position, MovementFunctions.GetTimeMovement(_entity, _unit.Target.position)).OnComplete(                    
                        delegate { _entity.transform.DOLookAt(_unit.Target.position, _entity.rotationSpeed);}
                        );
                    yield return null;
                }

                _unit.MovementBehaviourEnum = MovementBehaviourEnum.NULL_MOVEMENT;

                Unit _target = ((Unit)_unit.Target);

                // Definir une priorité ?

                _unit.IsAttackBy(_target);
                _target.IsAttackBy(_unit);
            }

            yield break;
        }
    }
}