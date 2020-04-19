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

                float d = 0f;

                if (_unit.Target != null)
                    d = Vector3.Distance(_entity.transform.position, _unit.Target.transform.position);

                while (d > distanceToCheck)
                {
                    if (_unit.Target == null)
                        yield break;

                    if (_unit.IsStatic)
                        yield return null;

                    if (_unit.Target != null)
                    {
                        d = Vector3.Distance(_entity.transform.position, _unit.Target.transform.position);
                        if(!_entity.IsStatic) _entity.rotationTween = _entity.transform.DOLookAt(_unit.Target.transform.position, _entity.rotationSpeed);

                        _entity.transform.position = Vector3.MoveTowards(_entity.transform.position, _unit.Target.transform.position, _entity.Speed * Time.deltaTime);
                    }
                    yield return null;
                }

                //Debug.Log(1 + _entity.name + " Reach " + _unit.Target);
                _unit.HaveReachTarget();
            }

            yield break;
        }
    }
}