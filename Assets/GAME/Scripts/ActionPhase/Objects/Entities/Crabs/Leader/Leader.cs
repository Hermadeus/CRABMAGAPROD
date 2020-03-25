using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class Leader : Unit
    {
        public bool asUsePassif = false;

        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.JOIN_CASTLE_MOVEMENT;
        }

        public void UsePassif()
        {
            if (asUsePassif)
                return;

            asUsePassif = true;

            passifBehaviour?.PassifEffect(this);
        }
    }
}