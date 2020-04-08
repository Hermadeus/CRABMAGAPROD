using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/CrabUnitData")]
    public class CrabUnitData : EntityData
    {
        [FoldoutGroup("Unit attribute")]
        public CrabUnitType type;

        [BoxGroup("Entity attribute")]
        public Sprite wheelThumbnail = default;

        [BoxGroup("Entity Formation")]
        public int formationX = 3;

        [BoxGroup("Entity Formation")]
        public int formationY = 3;

        [BoxGroup("Entity Formation")]
        public float density = 1f;
    }
}