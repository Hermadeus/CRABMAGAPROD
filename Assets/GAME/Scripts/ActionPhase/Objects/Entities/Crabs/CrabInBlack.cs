using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class CrabInBlack : CrabUnit, IStuntAttacker
    {
        [FoldoutGroup("Stunt Attribute")]
        [SerializeField] float stuntTimer = 1f;
        public float StuntTime { get => stuntTimer; set => stuntTimer = value; }
    }
}