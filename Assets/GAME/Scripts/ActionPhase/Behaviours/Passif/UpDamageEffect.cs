﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Passif/UP Damage Effect")]
    public class UpDamageEffect : BasePassifEffect
    {
        public override void PassifEffect(Unit unit)
        {
            unit.Damage++;
        }
    }
}