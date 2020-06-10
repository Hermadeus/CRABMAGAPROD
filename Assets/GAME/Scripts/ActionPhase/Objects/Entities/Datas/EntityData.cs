using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEditor;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/EntityData")]
    public class EntityData : ScriptableObject, ISavable
    {
        public bool isLock = true;

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
        public Triforce Triforce;

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
        [OnValueChanged("CalculateDPS")]
        public float damage = 0f;

        [BoxGroup("Entity attribute")]
        [Tooltip("Vitesse d'attaque de l'unité.")]
        [OnValueChanged("CalculateDPS")]
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

        [FoldoutGroup("Upgrade tab")]
        public UpgradeTab[] upgradeTabs = default;

        public Sprite[] pastilleDetection, pastilleDeath, pastilleOnInstantiation, pastilleOnReachCastle, pastilleAttack, pastilleOnLosePV;

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

            CalculateDPS();

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

        void CalculateDPS()
        {
            DamagePerSeconds = damage / attackSpeed;
        }

        public void Load()
        {
            currentLevel = PlayerPrefs.GetInt(entityName.textAnglais);            
        }

        public void Save()
        {
            PlayerPrefs.SetInt(entityName.textAnglais, currentLevel);
        }

        [Button]
        public virtual void UpgradeEntity()
        {
            Debug.Log("UPDATE " + entityName.textAnglais);
            
            currentLevel++;

            damage = upgradeTabs[currentLevel].damage;
            attackSpeed = upgradeTabs[currentLevel].attackSpeed;
            CalculateDPS();

            startHealth = upgradeTabs[currentLevel].health;
            currentPriceUpdate = upgradeTabs[currentLevel].upgradeCost;            

            //PersistableSO.Instance.Save();

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public TextAsset csvFile;

        [Button]
        public virtual void InitUpgradeTab()
        {
            int sz = 300;

            //upgradeTabs = new UpgradeTab[sz];

            string[,] s = ParseCSV();

            for (int i = 1; i < sz; i++)
            {
                string[] c1 = s[1, i].Split(';');
                float dmg = float.Parse(c1[0]);
                upgradeTabs[i].damage = dmg;

                string[] c2 = s[2, i].Split(';');
                float eff = float.Parse(c2[0]);
                upgradeTabs[i].formationX = Mathf.CeilToInt(Mathf.Sqrt(eff)) + 1;
                upgradeTabs[i].formationY = Mathf.FloorToInt(Mathf.Sqrt(eff)) - 1;

                string[] c3 = s[3, i].Split(';');
                int costF = int.Parse(c3[0]);
                upgradeTabs[i].costformation = costF;

                string[] c4 = s[4, i].Split(';');
                int costU = int.Parse(c4[0]);
                upgradeTabs[i - 1].upgradeCost = costU;

                upgradeTabs[i].health = 1;
                upgradeTabs[i].attackSpeed = attackSpeed;
            }

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

        public string[,] ParseCSV()
        {
            //split the data on split line character
            string[] lines = csvFile.text.Split("\n"[0]);

            // find the max number of columns
            int totalColumns = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(';');
                totalColumns = Mathf.Max(totalColumns, row.Length);
            }

            // creates new 2D string grid to output to
            string[,] outputGrid = new string[totalColumns + 1, lines.Length + 1];
            for (int y = 0; y < lines.Length; y++)
            {
                string[] row = lines[y].Split(';');
                for (int x = 0; x < row.Length; x++)
                {
                    outputGrid[x, y] = row[x];
                }
            }

            //Debug.Log("L = " + lines.Length);
            //Debug.Log("C = " + totalColumns);

            return outputGrid;
        }

        [Button]
        void ResetLevelOne()
        {
            currentLevel = 0;
            UpgradeEntity();
        }
    }


    public enum Triforce
    {
        AGILE,
        FORCE,
        RESISTANT
    }

    [System.Serializable]
    public class UpgradeTab
    {
        //attributes
        public float attackSpeed;
        public float damage;

        public float health;

        //couts
        public int costformation;
        public int upgradeCost;

        //effectif
        public int formationX;
        public int formationY;
    }
}