using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using DG.Tweening;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
        [FoldoutGroup("Gameplay References")]
        public CrabFormation crabFormationReference = default;

        public float castlePosZ = 0;

        public SpriteRenderer FOV_Obj;

        public Color FOV_Normal_Color;
        public Color FOV_Detect_Color;

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

            FOV_Obj.DOColor(FOV_Normal_Color, .5f);

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
            Health = 0;
            gameManager.castle.LosePV();
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

        protected override void Death()
        {
            base.Death();

            for (int i = 0; i < gameManager.crabUnitOnBattle.Count; i++)
            {
                gameManager.crabUnitOnBattle[i].onOtherUnitDie?.Invoke(gameManager.crabUnitOnBattle[i]);
            }
        }

        public SpriteRenderer ASFeedback;

        public void OnBoostAS()
        {
            ClignotementAlphaFeedback(ASFeedback);
        }

        public override void OnDetect()
        {
            base.OnDetect();
            FOV_Obj.DOColor(FOV_Detect_Color, .5f);
        }
    }
}