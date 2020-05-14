using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Nincrab : CrabUnit
    {
        public float esquivePercent = 0;

        public override void Init()
        {
            base.Init();

            esquivePercent = 0;
        }

        public override void ReceiveAttack(Unit attaquant, float _damage)
        {
            float p = Random.Range(0, 100);

            if(p > esquivePercent)
            {
                base.ReceiveAttack(attaquant, _damage);
            }
            else
            {
                Debug.Log("esquive");
            }
        }

        public void UpgardeEsquive()
        {
            if (esquivePercent + 15f > 90)
                return;

            esquivePercent += 15f;
        }

        protected override void Death()
        {
            for (int i = 0; i < crabFormationReference.CrabUnits.Count; i++)
            {
                if(crabFormationReference.CrabUnits[i] is Nincrab)
                {
                    UpgardeEsquive();
                }
            }

            base.Death();

            esquivePercent = 0f;
        }
    }
}