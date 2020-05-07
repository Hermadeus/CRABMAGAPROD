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

        public CastleToDefend CastleToDefend;

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

        protected override void Death()
        {
            base.Death();
            Debug.Log("death enemy");
        }

        public Collider[] hitColliders;

        public GuardHouseManager guardHouseManager = default;

        public override void Init()
        {
            guardHouseManager = FindObjectOfType<GuardHouseManager>();
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

            CastleToDefend = CastleToDefend.Instance;
        }

        public override void UpdateComportement()
        {
            base.UpdateComportement();

            if (IsStunt)
                Debug.Log("JE SUIS STUNT");

            if(CastleToDefend != null)
                if(transform.position.z < CastleToDefend.transform.position.z)
                {
                    ReachCastle();
                }
        }

        public void ReachCastle()
        {
            Debug.Log("reach castle !");
            Health = 0;
            CastleToDefend.LosePV(1);
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