using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Utilities;

namespace CrabMaga
{
    public class Unit : SerializedMonoBehaviour, IPoolable, IResetable
    {
        [BoxGroup("Data")] public UnitData unitData = default;

        [BoxGroup("Behaviours")]
        public IMovementBehaviour movementBehaviour = default;
        [BoxGroup("Behaviours")]
        public IDieBehaviour dieBehaviour = default;
        [BoxGroup("Behaviours")]
        public IAttackBehaviour attackBehaviour = default;
        [BoxGroup("Behaviours")]
        public IDetectBehaviour detectBehaviour = default;
        [BoxGroup("Behaviours")]
        public ISurroundBehaviour surroundBehaviour = default;

        [SerializeField, BoxGroup("Attributes")] int pv;
        public virtual int PV
        {
            get
            {
                return pv;
            }
            set
            {
                pv = value;
                if (pv <= 0)
                    dieBehaviour?.Die(this);
            }
        }
        
        bool isMoving = true;
        public bool IsMoving
        {
            get => isMoving;
            set
            {
                isMoving = value;
                if(value == true)
                {
                    movementBehaviour.RestartMove(this);
                }
                else
                {
                    movementBehaviour.StopMove(this);
                }
            }
        }
        
        [SerializeField, BoxGroup("Attributes"), ReadOnly] float speed = 0f;
        public float Speed { get => speed; set => speed = value; }

        bool isPool = false;
        public bool IsPool
        {
            get => isPool;
            set
            {
                isPool = value;
                if (value == true)
                    this.enabled = true;
                else
                    this.enabled = false;
            }
        }

        [SerializeField, FoldoutGroup("Detection Behaviour")] Collider[] unitDetectable;
        public Collider[] UnitDetectable
        {
            get => unitDetectable;
            set
            {
                unitDetectable = value;
                UnitTarget = detectBehaviour?.GetNearestUnitToAttack(this);
            }
        }

        [FoldoutGroup("Detection Behaviour")]
        public LayerMask layerMaskToDetect;
        [FoldoutGroup("Detection Behaviour")]
        public float detectableRange = 3f;
        [FoldoutGroup("Detection Behaviour")]
        public bool haveDetectUnit = false;

        private Unit previousUnitTarget;

        [SerializeField, FoldoutGroup("Detection Behaviour")] Unit unitTarget = default;
        public Unit UnitTarget
        {
            get => unitTarget;
            set
            {
                unitTarget = value;
                if(value != previousUnitTarget)
                {
                    if(value != null)
                    {
                        //Va vers l'unite
                        // FAIRE LE MOUVEMENT DE L'UNITE SELON UNE TARGET
                    }

                    previousUnitTarget = value;
                }
            }
        }

        public bool asSurround = false;

        public virtual int Damage { get => unitData.attackPower; }

        public PoolManager poolManager = default;

        #region Runtime Methods
        void Start()
        {
            Init();
        }

        private void Update()
        {
            UpdateComportement();
        }
        #endregion

        /// <summary>
        /// Played in Awake
        /// </summary>
        protected virtual void Init()
        {
            if(unitData != null)
            {
                PV = unitData.startPV;
            }

            if (movementBehaviour != null)
                Speed = movementBehaviour.BaseSpeed;
        }

        /// <summary>
        /// Played in Update
        /// Used to execute comportement at each frames
        /// </summary>
        protected virtual void UpdateComportement()
        {
            if (IsMoving)
                movementBehaviour?.Move(this);

            if(!haveDetectUnit)
                UnitDetectable = detectBehaviour?.DetectEnemies(this);

            if (UnitTarget == null)
                asSurround = false;
        }

        #region PoolSystem
        public virtual void Push()
        {
            Debug.Log("push");
            IsPool = false;

            transform.position = poolManager.poolPosition;
            poolManager.unitsQueue.Add(this);

            ResetObject();
        }
        #endregion

        public void HaveReachEnemy() //A mettre en strategy pattern
        {
            attackBehaviour?.Attack(this);
        }

        public virtual void GetDamage(int damage)
        {
            PV -= damage;
        }

        public virtual void ResetObject()
        {
            if(unitData != null)
            {
                PV = unitData.startPV;
            }
        }
    }
}