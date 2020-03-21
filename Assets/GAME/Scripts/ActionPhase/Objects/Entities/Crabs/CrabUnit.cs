using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
        public override Entity Target
        {
            get => base.Target;
            set
            {
                base.Target = value;

                if (value != null)
                {
                    movementBehaviour = entityData.behaviourSystem.GetMovementBehaviour(MovementBehaviourEnum.FOLLOW_TARGET_MOVEMENT);
                    movementBehaviour.Move(this);
                }
            }
        }

        public override void AsWin()
        {
            base.AsWin();
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