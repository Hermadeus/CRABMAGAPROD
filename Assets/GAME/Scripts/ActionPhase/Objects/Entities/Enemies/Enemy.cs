using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Enemy : Unit
    {
        public override Entity Target
        {
            get => base.Target;
            set
            {
                base.Target = value;

                if (value != null)
                {
                    MovementBehaviourEnum = MovementBehaviourEnum.FOLLOW_TARGET_MOVEMENT;
                }
            }
        }

        public Collider[] hitColliders;

        public override void AsWin()
        {
            base.AsWin();

            CheckAttackedBy();
            if (AttackedBy.Count == 0)
            {
                movementTween.onKill();
                MovementBehaviourEnum = MovementBehaviourEnum.TARGET_MOVEMENT;
            }
        }

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();
            hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, layerMaskTarget);            
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}