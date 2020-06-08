using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using DG.Tweening;

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

                if(value.Length > 0)
                {
                    OnDetect();
                }

                unitInRangeOfView = value;
            }
        }

        [FoldoutGroup("Debug")] public Pastille pastilleRef = default;

        [HideInInspector] public Coroutine attackCor;

        public PassifEvent passifEvent;

        [SerializeField] bool isStunt = false;
        public bool IsStunt
        {
            get => isStunt;
            set
            {
                isStunt = value;

                if(value == true)
                {
                    if(attackCor != null)
                        StopCoroutine(attackCor);
                }
                else
                {
                    if (attackCor != null)
                        StopCoroutine(attackCor);

                    if (Target != null)
                        Attack(this, Target);
                }
            }
        }

        public override void Init()
        {
            base.Init();
            InitPassif();

            StartCoroutine(InvokeInitEvent());            

            if (pastilleRef != null)
            {
                pastilleRef.AnimateOnInstantiation(entityData);
            }
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

            detectionBehaviour?.Detect(this);

            if (IsStatic || IsStunt)
                return;

            base.UpdateComportement();
            
        }

        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }

        void OnDrawGizmosSelected()
        {
            
        }

        public void Attack(Unit _unit, IAttackReceiver _target)
        {
            if (IsStatic || IsStunt)
                return;

            ///Effect
            attackBehaviour.Attack(_unit, _target);
            onAttack?.Invoke(this);

            if (pastilleRef != null)
            {
                pastilleRef.AnimateOnAttack(entityData);
            }
        }

        IEnumerator BattlePastille()
        {
            pastilleRef?.SetBackgroundPastille(entityData.pastilleCombatSprite);
            pastilleRef.outline.effectColor = new Color(pastilleRef.outline.effectColor.r, pastilleRef.outline.effectColor.g, pastilleRef.outline.effectColor.b, 1f);
            yield return new WaitForSeconds(1f);
            pastilleRef?.SetBackgroundPastille(entityData.pastilleSprite);

            if(pastilleRef != null)
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
            Attack(this, Target); //JE COMMENCE L'ATTAQUE
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

                case PassifEvent.ON_CLICK:
                    break;

                case PassifEvent.ON_OTHER_UNIT_DIE:
                    onOtherUnitDie.AddListener(PlayPassif);
                    break;
            }
        }

        void PlayPassif(Entity entity)
        {
            passifBehaviour?.PassifEffect(this);
            animator.SetTrigger("onUlt");
            OnPassifFeedback();
        }

        public void Stunt()
        {
            animator.SetTrigger("onStunt");
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
            SoundManager.instance.PlaySound(winSound, audiosource);
        }

        public void OnLose()
        {
            ReceiveAttack(this, 1000f);
        }

        //FEEDBACKS
        public virtual void OnPassifFeedback()
        {
            passifSound?.Play(audiosource);

        }

        public void ClignotementAlphaFeedback(SpriteRenderer sr)
        {
            sr.DOFade(1f, .5f).OnComplete(
                delegate { sr.DOFade(0f, .5f); }
                );
        }

        public virtual void OnDetect()
        {
            
        }
    }
}