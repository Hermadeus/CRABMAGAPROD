using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class EnemyAgile : Enemy
    {
        [BoxGroup("Collider Attack")]
        [SerializeField] Transform offset, offset1, offset2;
        [BoxGroup("Collider Attack")]
        [SerializeField] float radius = 1f;
        [BoxGroup("Collider Attack")]
        [SerializeField] Vector2 dimension = new Vector2();
        [BoxGroup("Collider Attack")]
        [SerializeField] float rotation = 1f;

        public override void FixedUpdateComportement()
        {
            if(attackBehaviour is AttackCollider )
                hitColliders = Col();

            base.FixedUpdateComportement();
        }

        Collider[] Col()
        {
            return /*Collider[] ca =*/ CircleCollider(offset.position, radius, layerMaskTarget);
            //Collider[] cb = LineCollider(offset1.position, dimension.x, dimension.y, rotation, layerMaskTarget);
            //Collider[] cc = LineCollider(offset2.position, dimension.x, dimension.y, -rotation, layerMaskTarget);

            //List<Collider> cols = new List<Collider>();

            //for (int i = 0; i < ca.Length; i++)
            //{
            //    for (int a = 0; a < cols.Count; a++)
            //    {
            //        if (ca[i] == cols[a])
            //            cols.Add(ca[i]);
            //    }
            //}

            //for (int i = 0; i < cb.Length; i++)
            //{
            //    for (int a = 0; a < cols.Count; a++)
            //    {
            //        if (cb[i] == cols[a])
            //            cols.Add(cb[i]);
            //    }
            //}

            //for (int i = 0; i < cc.Length; i++)
            //{
            //    for (int a = 0; a < cols.Count; a++)
            //    {
            //        if (cc[i] == cols[a])
            //            cols.Add(cc[i]);
            //    }
            //}

            //return cols.ToArray();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(offset.position, radius);

            Gizmos.matrix = Matrix4x4.TRS(offset1.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotation, transform.rotation.z), new Vector3(dimension.y, 1f, dimension.x));
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(dimension.y, 1f, dimension.x));

            Gizmos.matrix = Matrix4x4.TRS(offset2.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y - rotation, transform.rotation.z), new Vector3(dimension.y, 1f, dimension.x));
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(dimension.y, 1f, dimension.x));
        }
    }
}