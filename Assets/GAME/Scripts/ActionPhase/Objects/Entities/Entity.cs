using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Utilities;

using DG.Tweening;

namespace CrabMaga
{
    public class Entity : SerializedMonoBehaviour, IResetable
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
                movementBehaviour = entityData.behaviourSystem.GetMovementBehaviour(value);
            }
        }
    
        [FoldoutGroup("Attributes")]
        [SerializeField] float health = 0f;
        public float Health
        {
            get => health;
            set
            {
                health = value;
            }
        }

        [FoldoutGroup("Attributes")]
        [SerializeField] float damage = 0f;
        public float Damage
        {
            get => damage;
            set
            {
                damage = value;
            }
        }

        [FoldoutGroup("Attributes")]
        [SerializeField] float attackSpeed = 0f;
        public float AttackSpeed
        {
            get => attackSpeed;
            set
            {
                attackSpeed = value;
            }
        }

        [HideInInspector]
        public Tween movementTween;
        [HideInInspector]
        public Vector3 position => transform.position;

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

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            UpdateComportement();
        }

        public virtual void Init()
        {
            entityData.Init(this);

            movementBehaviour?.Move(this);
        }

        public virtual void UpdateComportement()
        {

        }

        public virtual void ResetObject()
        {
            throw new System.NotImplementedException();
        }
    }
}