using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Franckeincrab : Leader
    {
        public Transform offset;
        public float radius = 1;

        public Collider[] ennemiesInRange;

        public LayerMask ennemies;

        public float healthRewin = 1;
        public float ultTimer = 6;

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();

            ennemiesInRange = ColEnemies();
        }

        public override void UsePassif()
        {
            base.UsePassif();

            StartCoroutine(Effect());
        }

        public IEnumerator Effect()
        {
            for (int i = 0; i < ultTimer; i++)
            {
                ApplyEffect();

                yield return new WaitForSeconds(1f);
            }

            yield break;   
        }

        void ApplyEffect()
        {
            for (int i = 0; i < ennemiesInRange.Length; i++)
            {
                Enemy e = ennemiesInRange[i].GetComponentInParent<Enemy>();
                e.ReceiveAttack(this, Damage / 3);
            }

            if (health < entityData.startHealth)
                Health++;
        }

        Collider[] ColEnemies()
        {
            return CircleCollider(offset.position, radius, ennemies);
        }

        protected Collider[] CircleCollider(Vector3 position, float radius, LayerMask layerMask)
        {
            return Physics.OverlapSphere(
                position,
                radius,
                layerMask
                );
        }
    }
}