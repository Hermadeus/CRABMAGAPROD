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
            MovementBehaviourEnum = MovementBehaviourEnum.TARGET_MOVEMENT;
        }
    }

}