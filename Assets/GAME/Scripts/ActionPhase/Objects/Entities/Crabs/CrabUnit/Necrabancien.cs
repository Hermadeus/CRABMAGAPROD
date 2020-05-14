using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Necrabancien : CrabUnit
    {
        public float ressuciteTimer = 5f;

        protected override void Death()
        {
            if (crabFormationReference != null)
            {
                crabFormationReference.lastDeathPos = transform.position;
                crabFormationReference.Ressucite(ressuciteTimer);
            }

            base.Death();
        }
    }
}