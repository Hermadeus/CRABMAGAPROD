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

        public Collider[] unitInRangeOfView;

        public override void UpdateComportement()
        {
            base.UpdateComportement();

            if(Target == null)
                detectionBehaviour?.Detect(this);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, DetectionRange);
        }
    }
}