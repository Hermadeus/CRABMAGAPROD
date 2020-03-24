using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Crab : Unit
    {
        public CrabUnit crabUnitReference = default;

        protected override void Death()
        {
            crabUnitReference.ReceiveAttack(lastHitUnitReceive, Health);
            base.Death();
        }
    }
}