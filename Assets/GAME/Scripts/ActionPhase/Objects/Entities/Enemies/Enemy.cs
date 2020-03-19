using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Enemy : Unit
    {
        public override Entity Target
        {
            get => base.Target;
            set
            {
                base.Target = value;
                movementBehaviour = entityData.behaviourSystem.GetMovementBehaviour(MovementBehaviourEnum.FOLLOW_TARGET_MOVEMENT);
                movementBehaviour.Move(this);
            }
        }
    }
}