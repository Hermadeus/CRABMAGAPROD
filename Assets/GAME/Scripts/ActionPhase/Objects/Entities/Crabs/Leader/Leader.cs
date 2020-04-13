using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class Leader : Unit
    {
        public bool asUsePassif = false;

        public Slider sliderHealth = default;

        public float castlePosZ = 0;

        public override float Health
        {
            get => base.Health;
            set
            {
                base.Health = value;
                sliderHealth.value = value;
            }
        }

        public override void Init()
        {
            base.Init();

            sliderHealth.maxValue = entityData.startHealth;
            sliderHealth.value = sliderHealth.maxValue;

            castlePosZ = gameManager.castle.transform.position.z;
        }

        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.JOIN_CASTLE_MOVEMENT;
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

        public void UsePassif()
        {
            if (asUsePassif)
                return;

            animator.SetTrigger("onUlt");

            asUsePassif = true;

            passifBehaviour?.PassifEffect(this);
        }

        public override void OnPush()
        {
            base.OnPush();
            gameManager.CurrentUnitCountInt--;
        }
    }
}