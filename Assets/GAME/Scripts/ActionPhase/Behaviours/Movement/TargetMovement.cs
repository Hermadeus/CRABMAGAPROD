using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Movement/Target Movement")]
    public class TargetMovement : BaseMovement
    {
        public override void Move(Entity _entity)
        {
            base.Move(_entity);

            if (_entity.Destination != null)
                MovementFunctions.GoTo(_entity, _entity.Destination.transform.position, _entity.Speed, null, .1f);
            else
                throw new System.Exception(
                    string.Format("{0} n'a pas de destination.", _entity.gameObject.name)
                    );
        }
    }
}