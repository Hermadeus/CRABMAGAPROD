using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Enemy : Unit
    {
        public override Unit Target
        {
            get => base.Target;
            set
            {
                base.Target = value;

                if (Target != null)
                    StopCoroutine(movementCor);
            }
        }

        public Collider[] hitColliders;

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();
            hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, layerMaskTarget);            
        }

        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.TARGET_MOVEMENT;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}