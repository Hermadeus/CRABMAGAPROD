using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

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
                if (entityData != null)
                {
                    movementBehaviour = entityData.behaviourSystem.GetMovementBehaviour(value);
                    movementBehaviour?.Move(this);
                }
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
        [FoldoutGroup("References")]
        [SerializeField] Transform parentPoolingQueue = default;

        [FoldoutGroup("Events")]
        public EntityEvent
            onInit = new EntityEvent(),
            onDie = new EntityEvent(),
            onWin = new EntityEvent(),
            onAttack = new EntityEvent();

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
            lastHitUnitReceive?.WinCombat();

            onDie?.Invoke(this);

            poolingManager.Push(this);
        }

        public Unit lastHitUnitReceive;

        public void ReceiveAttack(Unit attaquant, float _damage)
        {
            lastHitUnitReceive = attaquant;
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

            transform.parent = parentPoolingQueue;

            ResetObject();
        }

        public virtual void ResetObject()
        {
            entityData = null;

            movementTween.Kill();
            movementTween = null;

            if(movementCor != null)
                StopCoroutine(movementCor);
            movementCor = null;

            movementBehaviour = null;
            Speed = 1000;
            Health = 10000;

            Destination = null;

            lastHitUnitReceive = null;
        }

        [Button]
        public virtual void InitButton()
        {
            gameManager = FindObjectOfType<AP_GameManager>();
            poolingManager = FindObjectOfType<PoolingManager>();
            parentPoolingQueue = transform.parent;
        }
    }

    [System.Serializable]
    public class EntityEvent : UnityEvent<Entity> { }
}