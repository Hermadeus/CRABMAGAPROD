using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Movement/Join Castle Movement")]
    public class JoinCastleMovement : BaseMovement
    {
        public LayerMask mask;

        public override void Move(Entity _entity)
        {
            base.Move(_entity);

            RaycastHit hit;

            if (Physics.Raycast(_entity.position, Vector3.forward, out hit, Mathf.Infinity, mask))
            {
                Debug.DrawRay(_entity.position, Vector3.forward * hit.distance, Color.yellow);
            }
            else
            {
                Debug.DrawRay(_entity.position, Vector3.forward * 1000, Color.white);
                throw new System.Exception(string.Format("{0} ne trouve pas le chateau !", _entity.name));
            }

            MovementFunctions.GoTo(_entity, hit.point, true);
        }
    }
}