using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class Crabpitaine : CrabUnit, IBoostSpeedAttackOnOther
    {
        [FoldoutGroup("Passif Attribute")]
        [SerializeField] float attackSpeedBoostTimer = 3f;
        [SerializeField] float attackSpeedMultiplier = 2f;

        public float AttackSpeedBoostTimer { get => attackSpeedBoostTimer; set => attackSpeedBoostTimer = value; }
        public float AttackSpeedMultiplier { get => attackSpeedMultiplier; set => attackSpeedMultiplier = value; }

        public override void Init()
        {
            base.Init();
        }
    }
}