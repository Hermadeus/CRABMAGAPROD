using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class EnemyResistant : Enemy
    {
        [BoxGroup("Collider Attack")]
        [SerializeField] private Vector2 dimension = new Vector2(.3f, 1f);
        [BoxGroup("Collider Attack")]
        [SerializeField] private Transform offset;
        [BoxGroup("Collider Attack")]
        [SerializeField] private float rotation = 45f;

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();

            if (attackBehaviour is AttackCollider)
                hitColliders = LineCollider(offset.position, dimension.x, dimension.y, rotation, layerMaskTarget);
        }

        void OnDrawGizmos()
        {
            Gizmos.matrix = Matrix4x4.TRS(offset.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotation, transform.rotation.z), new Vector3(dimension.y, 1f, dimension.x));
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(dimension.y, 1f, dimension.x));
        }
    }
}