using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [SelectionBase]
    public class Unit : Entity, IAttack, IStuntable, IPastillable
    {
        [FoldoutGroup("Comportements")]
        public IDetectSomethingBehaviour detectionBehaviour = default;
        [FoldoutGroup("Comportements")]
        public IAttackBehaviour attackBehaviour = default;
        [FoldoutGroup("Comportements")]
        public IPassifBehaviour passifBehaviour = default;

        [FoldoutGroup("Attributes")]
        [SerializeField] protected float damage = 0f;
        public virtual float Damage
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

        [FoldoutGroup("Attributes")]
        [SerializeField] float detectionRange = 0f;
        public float DetectionRange
        {
            get => detectionRange;
            set
            {
                detectionRange = value;
            }
        }

        [FoldoutGroup("Attributes")]
        public CrabUnitType crabUnitType;

        private Unit previousTarget = null;

        [FoldoutGroup("Gameplay References")]
        [SerializeField] Unit target = default;
        public virtual Unit Target
        {
            get => target;
            set
            {
                target = value;

                if(value != previousTarget)
                {
                    MovementBehaviourEnum = MovementBehaviourEnum.FOLLOW_TARGET_MOVEMENT;
                    previousTarget = value;
                }
            }
        }
        
        [FoldoutGroup("Attributes")]
        public EntityType entityType = EntityType.CRAB_UNIT;
        [FoldoutGroup("Attributes")]
        public EntityType favoriteTarget = EntityType.CRAB_UNIT;
        [FoldoutGroup("Attributes")]
        public LayerMask layerMaskTarget = default;

        [FoldoutGroup("Debug"), SerializeField] protected Collider[] unitInRangeOfView;
        public virtual Collider[] UnitInRangeOfView
        {
            get => unitInRangeOfView;
            set
            {
                if (value.Length != unitInRangeOfView.Length)
                    OnUnitRangeDetectionReachZero();

                unitInRangeOfView = value;
            }
        }

        [FoldoutGroup("Debug")] public Pastille pastilleRef = default;

        [HideInInspector] public Coroutine attackCor;

        public PassifEvent passifEvent;

        [SerializeField] bool isStunt = false;
        public bool IsStunt { get => isStunt; set => isStunt = value; }

        [SerializeField] bool isStatic = false;
        public bool IsStatic { get => isStatic; set => isStatic = value; }

        public override void Init()
        {
            base.Init();
            InitPassif();

            StartCoroutine(InvokeInitEvent());
        }

        protected IEnumerator InvokeInitEvent()
        {
            yield return new WaitForEndOfFrame();

            onInit?.Invoke(this);
            yield break;
        }

        public override void UpdateComportement()
        {
            pastilleRef?.SetHeight(transform.position.z);

            if (IsStunt || IsStatic)
                return;

            base.UpdateComportement();
            
            detectionBehaviour?.Detect(this);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }

        public void Attack(Unit _unit, IAttackReceiver _target)
        {
            if (IsStunt || IsStatic)
                return;

            ///Effect
            attackBehaviour.Attack(_unit, _target);
            onAttack?.Invoke(this);

            if (pastilleRef != null)
            {
                StartCoroutine(BattlePastille());
            }
        }

        IEnumerator BattlePastille()
        {
            pastilleRef?.SetBackgroundPastille(entityData.pastilleCombatSprite);
            pastilleRef.outline.effectColor = new Color(pastilleRef.outline.effectColor.r, pastilleRef.outline.effectColor.g, pastilleRef.outline.effectColor.b, 1f);
            yield return new WaitForSeconds(1f);
            pastilleRef?.SetBackgroundPastille(entityData.pastilleSprite);
            pastilleRef.outline.effectColor = new Color(pastilleRef.outline.effectColor.r, pastilleRef.outline.effectColor.g, pastilleRef.outline.effectColor.b, 0f);

            yield break;
        }

        public override void ResetObject()
        {
            base.ResetObject();
            Target = null;

            onDie.RemoveAllListeners();

            if(attackCor != null)
                StopCoroutine(attackCor);

            attackCor = null;

            IsStunt = false;

            if(pastilleRef != null)
            {
                pastilleRef.IsUsed = false;
                pastilleRef = null;
            }
        }

        public void HaveReachTarget()
        {
            MovementBehaviourEnum = MovementBehaviourEnum.NULL_MOVEMENT;
        }

        protected virtual void OnUnitRangeDetectionReachZero()
        {
            Target = null;
        }

        public void InitPassif()
        {
            switch (passifEvent)
            {
                case PassifEvent.NEVER:
                    break;
                case PassifEvent.ON_INSTANTIATION:
                    onInit.AddListener(PlayPassif);

                    break;
                case PassifEvent.ON_DIE:
                    onDie.AddListener(PlayPassif);

                    break;
                case PassifEvent.ON_WIN:
                    onWin.AddListener(PlayPassif);

                    break;
                case PassifEvent.ON_ATTACK:
                    onAttack.AddListener(PlayPassif);

                    break;
            }
        }

        void PlayPassif(Entity entity)
        {
            passifBehaviour.PassifEffect(this);
        }

        public void Stunt()
        {
            
        }

        public virtual void WinCombat()
        {
            onWin?.Invoke(this);            
        }

        public void SetPastille()
        {
            pastilleRef = gameManager.cameraSlider.AddPastille(transform.position.z, entityData.pastilleSprite);
        }

        public void OnWin()
        {
            winSound?.Play(audiosource);
        }

        public void OnLose()
        {
            ReceiveAttack(this, 1000f);
        }
    }
}