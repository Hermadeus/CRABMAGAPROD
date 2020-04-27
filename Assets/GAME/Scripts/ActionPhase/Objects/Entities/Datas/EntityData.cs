using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEditor;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/EntityData")]
    public class EntityData : ScriptableObject
    {
        [FoldoutGroup("References")]
        public BehavioursSystem behaviourSystem = default;
        [FoldoutGroup("References")]
        public LanguageManager languageManager = default;

        #region Attribute
        [BoxGroup("Entity attribute")]
        [Tooltip("Nom de l'entité.")]
        public StringLanguage entityName;

        [BoxGroup("Entity attribute")]
        public TextLanguage entityDescription = default;

        [BoxGroup("Entity attribute")]
        public int currentLevel = 1;

        [FoldoutGroup("Passif attribute"), TextArea(3, 5)]
        public TextLanguage passifDescription = default;

        [BoxGroup("Entity attribute")]
        [Tooltip("Référence au préfab.")]
        public Unit unitType = default;

        [BoxGroup("Entity attribute")]
        public Sprite entityicon;

        [BoxGroup("Entity attribute")]
        [Tooltip("Mouvement au départ de l'AP.")]
        public MovementBehaviourEnum startMovementBehaviour = MovementBehaviourEnum.TARGET_MOVEMENT;
        
        //------------------------

        [BoxGroup("Entity attribute")]
        [Tooltip("Point de vie au départ de l'AP.")]
        public float startHealth = 0f;

        [BoxGroup("Entity attribute")]
        [Tooltip("Vitesse de base de l'unité")]
        public SpeedEnum speedEnum = SpeedEnum.STATIC;

        [ReadOnly, BoxGroup("Entity attribute")]
        [Tooltip("Vitesse de base de l'unité.")]
        public float baseSpeed = 0f;
        
        [BoxGroup("Entity attribute")]
        [Tooltip("Vitesse de rotation de l'unité.")]
        public float rotationSpeed = .5f;

        [BoxGroup("Entity attribute")]
        [Tooltip("Acceleration de l'unité.")]
        public AccelerationEnum accelerationEnum = AccelerationEnum.MEDIUM;

        [ReadOnly, BoxGroup("Entity attribute")]
        [Tooltip("Acceleration de l'unité.")]
        public float acceleration = 0f;

        [BoxGroup("Entity attribute")]
        [Tooltip("Force d'attaque de l'unité.")]
        public float damage = 0f;

        [BoxGroup("Entity attribute")]
        [Tooltip("Vitesse d'attaque de l'unité.")]
        public float attackSpeed = 0f;

        [ReadOnly, BoxGroup("Entity attribute")]
        [Tooltip("Dégâts par secondes de l'unité.")]
        public float DamagePerSeconds = 0f;

        [BoxGroup("Entity attribute")]
        [Tooltip("Range de detection de l'unité.")]
        public DetectionRangeEnum detectionRange = DetectionRangeEnum.MEDIUM;
                
        [FoldoutGroup("Unit attribute")]
        [Tooltip("Comportement de l'unité à la detection d'autres unités.")]
        public DetectionBehaviourEnum detectionBehaviour = DetectionBehaviourEnum.CLOSEST_DETECTION;

        [FoldoutGroup("Unit attribute")]
        [Tooltip("Type de l'unité.")]        
        public EntityType entityType = EntityType.CRAB_UNIT;

        [FoldoutGroup("Unit attribute")]
        [Tooltip("Favorite target.")]
        public EntityType favoriteTarget = EntityType.CRAB_UNIT;

        [FoldoutGroup("Unit attribute")]
        [Tooltip("Sert à la detection d'autres unités.")]
        public LayerMask layerMaskTarget = default;

        [FoldoutGroup("Unit attribute")]
        [Tooltip("Type d'attaque de l'unité.")]
        public AttackEnum attackType = AttackEnum.SIMPLE_ATTACK;
        #endregion

        [BoxGroup("Entity Pastille")]
        public bool havePastille = false;
        [BoxGroup("Entity Pastille")]
        public Sprite pastilleSprite = default;
        [BoxGroup("Entity Pastille")]
        public Sprite pastilleCombatSprite = default;
                
        [FoldoutGroup("Passif attribute")]
        public PassifBehaviourEnum passifBehaviour = PassifBehaviourEnum.NULL_PASSIF;
        [FoldoutGroup("Passif attribute")]
        public PassifEvent passifEvent = PassifEvent.NEVER;

        public int currentPriceUpdate = 20;

        public virtual void Init(Entity entity)
        {
            entity.name = entityName.GetCurrentText(languageManager.LanguageEnum);

            baseSpeed = behaviourSystem.GetSpeed(speedEnum);
            acceleration = behaviourSystem.GetAcceleration(accelerationEnum);
            
            entity.Health = startHealth;
            entity.MovementBehaviourEnum = startMovementBehaviour;

            entity.Speed = baseSpeed;
            entity.rotationSpeed = rotationSpeed;

            entity.movementBehaviour = behaviourSystem.GetMovementBehaviour(startMovementBehaviour);

            DamagePerSeconds = damage / attackSpeed;

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

        public void UpgradeEntity()
        {
            Debug.Log("UPDATE " + entityName.textAnglais);


            currentLevel++;

            PersistableSO.Instance.Save();

        }
    }
}