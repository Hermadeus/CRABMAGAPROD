using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class Enemy : Unit
    {
        [SerializeField] private Slider sliderHealth = default;

        public override Unit Target
        {
            get => base.Target;
            set
            {
                base.Target = value;

                if (Target != null && movementCor != null)
                    StopCoroutine(movementCor);
            }
        }

        public override float Health { get => base.Health;
            set
            {
                base.Health = value;
                sliderHealth.value = value;
            }
        }

        public Collider[] hitColliders;

        public GuardHouseManager guardHouseManager = default;

        public override void Init()
        {
            GuardHouse gh = guardHouseManager?.GetNextEmptyGuardHouse();
            if(gh == null)
            {
                poolingManager?.Push(this);
                return;
            }
            Destination = gh;

            base.Init();

            sliderHealth.maxValue = entityData.startHealth;
            sliderHealth.value = sliderHealth.maxValue;
        }

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();
            hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, layerMaskTarget);            
        }

        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.TARGET_MOVEMENT;
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }

        public override void InitButton()
        {
            base.InitButton();
            guardHouseManager = FindObjectOfType<GuardHouseManager>();
        }

        public override void OnPool()
        {
            base.OnPool();
            gameManager.enemiesOnBattle.Add(this);
        }

        public override void OnPush()
        {
            gameManager.enemiesOnBattle.Remove(this);

            if(Destination != null)
                Destination.isOccupy = false;

            base.OnPush();
        }
    }
}