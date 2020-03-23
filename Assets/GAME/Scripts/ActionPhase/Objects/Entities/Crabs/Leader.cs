using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Leader : Unit
    {
        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.JOIN_CASTLE_MOVEMENT;
        }
    }
}