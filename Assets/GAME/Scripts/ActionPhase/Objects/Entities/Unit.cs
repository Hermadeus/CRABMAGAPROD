using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [SelectionBase]
    public class Unit : Entity
    {
        [FoldoutGroup("Comportements")]
        public IDetectSomethingBehaviour detectionBehaviour = default;
        [FoldoutGroup("Comportements")]
        public IAttackBehaviour attackBehaviour = default;

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

        [FoldoutGroup("Gameplay References")]
        [SerializeField] Entity target = default;
        public virtual Entity Target
        {
            get => target;
            set
            {
                target = value;
            }
        }
        
        [FoldoutGroup("Attributes")]
        public EntityType entityType = EntityType.CRAB_UNIT;
        [FoldoutGroup("Attributes")]
        public EntityType favoriteTarget = EntityType.CRAB_UNIT;
        [FoldoutGroup("Attributes")]
        public LayerMask layerMaskTarget = default;

        [FoldoutGroup("Debug"), SerializeField] Collider[] unitInRangeOfView;
        public virtual Collider[] UnitInRangeOfView
        {
            get => unitInRangeOfView;
            set
            {
                unitInRangeOfView = value;                
            }
        }

        [FoldoutGroup("Gameplay References")]
        [SerializeField] private List<Unit> attackedBy = new List<Unit>();
        public List<Unit> AttackedBy
        {
            get => attackedBy;
            set
            {
                attackedBy = value;
                CheckAttackedBy();
            }
        }
        
        [HideInInspector] public Coroutine attackCor;
        public bool isAttacking = false;

        [FoldoutGroup("Debug")] public bool isTarget = false;

        public override void UpdateComportement()
        {
            base.UpdateComportement();

            if(Target == null)
                detectionBehaviour?.Detect(this);

            if (UnitInRangeOfView.Length > 0 && UnitInRangeOfView != null)
                Check();
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }

        public void IsAttackBy(Unit _unit)
        {
            AttackedBy.Add(_unit);

            CheckAttackedBy();

            MovementBehaviourEnum = MovementBehaviourEnum.NULL_MOVEMENT;

            if (!isAttacking)
            {
                attackBehaviour.Attack(this, Target);
            }
        }

        public virtual void AsWin()
        {
            Target = null;
            isTarget = false;

            isAttacking = false;
        }

        protected override void Death()
        {
            if (AttackedBy != null && AttackedBy.Count > 0)
            {
                for (int i = 0; i < AttackedBy.Count; i++)
                    AttackedBy[i].AsWin();
            }                

            base.Death();
        }

        void Check()
        {
            if (UnitInRangeOfView.Length == 0 || Target != null)
                return;

            int x = 0;
            float bestDist = 1000f;

            Unit t = null;

            for (int i = 0; i < UnitInRangeOfView.Length; i++)
            {
                if (UnitInRangeOfView[i].GetComponentInParent<Unit>().entityType == favoriteTarget)
                {
                    t = UnitInRangeOfView[i].GetComponentInParent<Unit>();
                    if (t.isTarget || t == null)
                        if (Vector3.Distance(transform.position, UnitInRangeOfView[i].transform.position) < bestDist)
                        {
                            x = i;
                            bestDist = Vector3.Distance(transform.position, UnitInRangeOfView[i].transform.position);
                        }

                    t.isTarget = true;
                    Target = t;

                    return;
                }
            }

            t = UnitInRangeOfView[x].GetComponentInParent<Unit>();

            if (t == null)
                return;

            if (t.isTarget)
                return;

            t.isTarget = true;
            Target = t;
        }

        protected void CheckAttackedBy()
        {
            for (int i = 0; i < AttackedBy.Count; i++)
            {
                if (AttackedBy[i] == null)
                    AttackedBy.RemoveAt(i);
            }
        }
    }
}