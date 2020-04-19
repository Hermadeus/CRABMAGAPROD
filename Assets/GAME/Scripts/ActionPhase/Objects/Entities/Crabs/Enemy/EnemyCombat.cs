using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class EnemyCombat : Enemy
    {
        [BoxGroup("Collider Attack")]
        [SerializeField] private Transform offset;
        [BoxGroup("Collider Attack")]
        [SerializeField] private float radius = 2f;

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();

            if (attackBehaviour is AttackCollider)
                hitColliders = CircleCollider(offset.position, radius, layerMaskTarget);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(offset.position, radius);
        }
    }
}