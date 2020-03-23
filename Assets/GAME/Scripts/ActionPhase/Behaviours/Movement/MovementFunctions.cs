using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;
using System;

namespace CrabMaga
{
    public static class MovementFunctions
    {
        public static void GoTo(Entity _entity, Vector3 destinationPoint, bool rotationAndMove = false)
        {
            if (_entity == null)
                return;

            if (!rotationAndMove)
            {
                _entity.transform.DOLookAt(destinationPoint, _entity.rotationSpeed);
                _entity.movementTween = _entity.transform.DOMove(destinationPoint, GetTimeMovement(_entity, destinationPoint));
            }
            else
            {
                _entity.movementTween =
                    _entity.transform.DOLookAt(destinationPoint, _entity.rotationSpeed).OnComplete(delegate {
                    _entity.movementTween = _entity.transform.DOMove(destinationPoint, GetTimeMovement(_entity, destinationPoint));
                });
            }
        }

        public static void GoTo(Entity _entity, Vector3 _destinationPoint, float _speed, Action actionWhenReach, float distanceToCheck = .5f)
        {
            _entity.movementCor = _entity.StartCoroutine(GoToCor(_entity, _destinationPoint, _speed, actionWhenReach, distanceToCheck));
        }

        public static IEnumerator GoToCor(Entity _entity, Vector3 _destinationPoint, float _speed, Action actionWhenReach, float distanceToCheck = .5f)
        {
            if (_entity is Unit)
            {
                Unit _unit = _entity as Unit;

                float d = Vector3.Distance(_entity.transform.position, _destinationPoint);

                while (d > distanceToCheck)
                {
                    if (_destinationPoint == null || _unit.Target != null)
                        yield break;

                    d = Vector3.Distance(_entity.transform.position, _destinationPoint);
                    _entity.transform.DOLookAt(_destinationPoint, _entity.rotationSpeed);

                    _entity.transform.position = Vector3.MoveTowards(_entity.transform.position, _destinationPoint, _entity.Speed * Time.deltaTime);

                    yield return null;
                }

                //Debug.Log("REACH");
                actionWhenReach?.Invoke();
            }

            yield break;
        }

        public static float GetTimeMovement(Entity _entity, Vector3 _pointToReach)
        {
            float t = 0f;
            t = Vector3.Distance(_pointToReach, _entity.transform.position);
            t /= _entity.entityData.baseSpeed;
            return t;
        }
    }
}