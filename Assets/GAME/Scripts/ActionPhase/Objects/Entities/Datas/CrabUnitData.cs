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
    }
}