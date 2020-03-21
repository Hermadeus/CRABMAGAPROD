using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Utilities;

using DG.Tweening;

namespace CrabMaga
{
    public class Entity : SerializedMonoBehaviour,
        IResetable, IAttackReceiver, IPoolable, IPushable
    {
        public EntityData entityData = default;

        [FoldoutGroup("Comportements")]
        public IMovementBehaviour movementBehaviour = default;

        [FoldoutGroup("Attributes")]
        [SerializeField] float speed = 0f;
        public float Speed
        {
            get => speed;
            set
            {
                speed = value;
            }
        }

        [FoldoutGroup("Attributes")]
        public float rotationSpeed = 0.5f;

        [FoldoutGroup("Attributes")]
        [SerializeField] MovementBehaviourEnum movementBehaviourEnum;
        public MovementBehaviourEnum MovementBehaviourEnum
        {
            get => movementBehaviourEnum;
            set
            {
                movementBehaviourEnum = value;
                movementTween.Kill();
                if(movementCor != null) StopCoroutine(movementCor);
                movementBehaviour = entityData.behaviourSystem.GetMovementBehaviour(value);
                movementBehaviour.Move(this);
            }
        }
    
        [FoldoutGroup("Attributes")]
        [SerializeField] protected float health = 0f;
        public virtual float Health
        {
            get => health;
            set
            {
                health = value;
                if (value <= 0)
                {
                    Death();

                }
            }
        }

        [HideInInspector]
        public Tween movementTween;
        [HideInInspector]
        public Coroutine movementCor;

        [FoldoutGroup("References")]
        public AP_GameManager gameManager = default;
        [FoldoutGroup("References")]
        public PoolingManager poolingManager = default;

        [FoldoutGroup("Gameplay References")]
        [SerializeField] Transform destination = default;
        public Transform Destination
        {
            get => destination;
            set
            {
                destination = value;
            }
        }

        [FoldoutGroup("Debug"), SerializeField] bool initALaMano = false;

        private void Awake()
        {
            if (initALaMano)
                Init();
        }

        private void Update()
        {
            UpdateComportement();
        }

        private void FixedUpdate()
        {
            FixedUpdateComportement();
        }

        public virtual void Init()
        {
            entityData?.Init(this);

            movementBehaviour?.Move(this);
        }

        public virtual void UpdateComportement()
        {

        }

        public virtual void FixedUpdateComportement()
        {

        }

        protected virtual void Death()
        {
            poolingManager.Push(this);
        }

        public void ReceiveAttack(float _damage)
        {
            Health -= _damage;
        }

        public virtual void OnPool()
        {
            Init();
        }

        public virtual void OnPush()
        {
            if (this.enabled == false)
                return;

            ResetObject();
        }

        public virtual void ResetObject()
        {
            entityData = null;

            movementTween.Kill();
            movementTween = null;
            StopAllCoroutines();
            movementCor = null;

            movementBehaviour = null;
            Speed = 1000;
            Health = 10000;

            Destination = null;
        }
    }
}