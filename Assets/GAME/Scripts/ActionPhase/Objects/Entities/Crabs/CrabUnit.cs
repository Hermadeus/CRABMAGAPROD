using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
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
    }

    [System.Serializable]
    public class CrabGroup
    {
        [SerializeField] private Crab crab = default;
        public Vector3 relativePosition = new Vector3();
        [HideInInspector] public CrabUnit crabUnit;

        public Crab Crab
        {
            get => crab;
            set
            {
                crab = value;                
            }
        }
    }
}