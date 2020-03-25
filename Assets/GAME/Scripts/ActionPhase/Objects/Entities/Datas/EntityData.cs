using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/EntityData")]
    public class EntityData : ScriptableObject
    {
        [BoxGroup("References")]
        public BehavioursSystem behaviourSystem = default;

        [BoxGroup("Entity attribute")]
        public string entityName = "";
        [BoxGroup("Entity attribute")]
        public Unit crabUnitType = default;

        [BoxGroup("Entity attribute")]
        public MovementBehaviourEnum startMovementBehaviour = MovementBehaviourEnum.TARGET_MOVEMENT;
        
        [BoxGroup("Entity attribute")]
        public float startHealth = 0f;
        [ReadOnly, BoxGroup("Entity attribute")]
        public float baseSpeed = 0f;
        [BoxGroup("Entity attribute")]
        public SpeedEnum speedEnum = SpeedEnum.STATIC;
        [BoxGroup("Entity attribute")]
        public float rotationSpeed = .5f;
        [ReadOnly, BoxGroup("Entity attribute")]
        public float acceleration = 0f;
        [BoxGroup("Entity attribute")]
        public AccelerationEnum accelerationEnum = AccelerationEnum.MEDIUM;

        [BoxGroup("Entity attribute")]
        public float damage = 0f;
        [BoxGroup("Entity attribute")]
        public float attackSpeed = 0f;
        [BoxGroup("Entity attribute")]
        public DetectionRangeEnum detectionRange = DetectionRangeEnum.MEDIUM;
                
        [FoldoutGroup("Unit attribute")]
        public DetectionBehaviourEnum detectionBehaviour = DetectionBehaviourEnum.CLOSEST_DETECTION;

        [FoldoutGroup("Unit attribute")]
        public EntityType entityType = EntityType.CRAB_UNIT;
        [FoldoutGroup("Unit attribute")]
        public EntityType favoriteTarget = EntityType.CRAB_UNIT;
        [FoldoutGroup("Unit attribute")]
        public LayerMask layerMaskTarget = default;
        [FoldoutGroup("Unit attribute")]
        public AttackEnum attackType = AttackEnum.SIMPLE_ATTACK;

        [FoldoutGroup("Passif attribute")]
        public PassifBehaviourEnum passifBehaviour = PassifBehaviourEnum.NULL_PASSIF;
        [FoldoutGroup("Passif attribute")]
        public PassifEvent passifEvent = PassifEvent.NEVER;

        public virtual void Init(Entity entity)
        {
            entity.name = entityName;

            baseSpeed = behaviourSystem.GetSpeed(speedEnum);
            acceleration = behaviourSystem.GetAcceleration(accelerationEnum);
            
            entity.Health = startHealth;
            entity.MovementBehaviourEnum = startMovementBehaviour;

            entity.Speed = baseSpeed;
            entity.rotationSpeed = rotationSpeed;

            entity.movementBehaviour = behaviourSystem.GetMovementBehaviour(startMovementBehaviour);

            if(entity is Unit)
            {
                Unit unit = entity as Unit;

                unit.detectionBehaviour = behaviourSystem.GetDetectionBehaviour(detectionBehaviour);
                unit.DetectionRange = behaviourSystem.GetDetectionRange(detectionRange);

                unit.Damage = damage;
                unit.AttackSpeed = attackSpeed;
                unit.attackBehaviour = behaviourSystem.GetAttackState(attackType);

                unit.entityType = entityType;
                unit.favoriteTarget = favoriteTarget;
                unit.layerMaskTarget = layerMaskTarget;

                unit.passifBehaviour = behaviourSystem.GetPassifBehaviour(passifBehaviour);
                unit.passifEvent = passifEvent;
            }
        }
    }
}