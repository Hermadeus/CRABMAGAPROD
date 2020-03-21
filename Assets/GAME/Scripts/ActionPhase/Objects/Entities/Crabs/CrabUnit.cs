using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
        [FoldoutGroup("Gameplay References")]
        public CrabFormation crabFormationReference = default;

        public override Unit Target
        {
            get => base.Target;
            set
            {
                base.Target = value;
            }
        }

        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.JOIN_CASTLE_MOVEMENT;
        }

        public override void ResetObject()
        {
            base.ResetObject();

            crabFormationReference = null;
        }
    }
}