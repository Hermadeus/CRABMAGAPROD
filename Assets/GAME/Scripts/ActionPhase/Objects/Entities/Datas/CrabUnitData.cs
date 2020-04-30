using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;
using UnityEditor;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/CrabUnitData")]
    public class CrabUnitData : EntityData
    {
        [FoldoutGroup("Unit attribute")]
        public CrabUnitType type;

        [FoldoutGroup("Unit attribute")]
        public int costUnit = 5;

        [BoxGroup("Entity attribute")]
        public Sprite wheelThumbnail = default;

        [BoxGroup("Entity Formation")]
        public int formationX = 3;

        [BoxGroup("Entity Formation")]
        public int formationY = 3;

        [BoxGroup("Entity Formation")]
        public float density = 1f;

        public override void UpgradeEntity()
        {
            base.UpgradeEntity();

            costUnit = upgradeTabs[currentLevel].costformation;
            formationX = upgradeTabs[currentLevel].formationX;
            formationY = upgradeTabs[currentLevel].formationY;

            PersistableSO.Instance.Save();

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }

    }
}