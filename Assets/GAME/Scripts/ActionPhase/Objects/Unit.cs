using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;


namespace CrabMaga
{
    public class Unit : SerializedMonoBehaviour, IPoolable
    {
        [BoxGroup("Behaviours")]
        public IMovementBehaviour movementBehaviour = default;
        [BoxGroup("Behaviours")]
        public IDieBehaviour dieBehaviour = default;
        [BoxGroup("Behaviours")]
        public IAttackBehaviour attackBehaviour = default;
        public IDetectBehaviour detectBehaviour = default;

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
        
        [SerializeField, BoxGroup("Attributes")] float speed = 0f;
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

        [SerializeField] Collider[] unitDetectable;
        public Collider[] UnitDetectable
        {
            get => unitDetectable;
            set
            {
                unitDetectable = value;
                UnitTarget = detectBehaviour?.GetNearestUnitToAttack(this);
            }
        }

        public LayerMask layerMaskToDetect;
        public float detectableRange = 3f;

        public bool haveDetectUnit = false;

        private Unit previousUnitTarget;

        [SerializeField] Unit unitTarget = default;
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
                        Debug.Log("Detect New Target");
                        //Va vers l'unite
                    }

                    previousUnitTarget = value;
                }
            }
        }

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
            if(movementBehaviour != null)
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
        }

        #region PoolSystem
        public void Push()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}