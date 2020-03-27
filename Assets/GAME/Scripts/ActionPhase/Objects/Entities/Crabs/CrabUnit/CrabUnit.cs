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

        public float castlePosZ = 0; 

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

            crabFormationReference?.CrabUnits.Remove(this);
            crabFormationReference?.TestDeath();
            crabFormationReference = null;
        }

        public override void Init()
        {
            base.Init();

            castlePosZ = gameManager.castle.transform.position.z;
        }

        public override void UpdateComportement()
        {
            base.UpdateComportement();
            CheckReachCastle();
        }

        void CheckReachCastle()
        {
            if (transform.position.z >= castlePosZ)
                ReachCastle();
        }

        public void ReachCastle()
        {
            poolingManager.Push(this);
            gameManager.CurrentScore++;
        }

        public override void OnPush()
        {
            base.OnPush();

            gameManager.crabUnitOnBattle.Remove(this);
        }

        public override void InitButton()
        {
            base.InitButton();
        }
    }
}