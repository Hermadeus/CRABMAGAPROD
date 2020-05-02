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

            //AttackSpeed = AttackSpeed ;
            Health = Health + gameManager.levelData.LevelIndex;

            sliderHealth.maxValue = entityData.startHealth;
            sliderHealth.value = sliderHealth.maxValue;
        }

        public override void UpdateComportement()
        {
            base.UpdateComportement();

            if (IsStunt)
                Debug.Log("JE SUIS STUNT");
        }

        protected override void OnUnitRangeDetectionReachZero()
        {
            base.OnUnitRangeDetectionReachZero();
            MovementBehaviourEnum = MovementBehaviourEnum.TARGET_MOVEMENT;
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

        public override void FixedUpdateComportement()
        {
            base.FixedUpdateComportement();
        }

        protected Collider[] LineCollider(Vector3 position, float longueur, float largueur, float rotation, LayerMask layermask)
        {
            return Physics.OverlapBox(
                position,
                new Vector3(largueur, 1f, longueur),
                Quaternion.Euler(transform.rotation.x, transform.rotation.y + rotation, transform.rotation.z),
                layerMaskTarget);
        }

        protected Collider[] CircleCollider(Vector3 position, float radius, LayerMask layerMask)
        {
            return Physics.OverlapSphere(
                position,
                radius,
                layerMask
                );
        }
    }
}