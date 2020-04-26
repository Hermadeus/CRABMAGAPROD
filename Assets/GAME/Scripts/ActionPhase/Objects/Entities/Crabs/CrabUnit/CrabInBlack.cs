using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class CrabInBlack : CrabUnit, IStuntAttacker
    {
        [FoldoutGroup("Passif Attribute")]
        float stuntTimer = 1f;
        public float StuntTime { get => stuntTimer; set => stuntTimer = value; }

        [SerializeField] bool asStunt = false;
        public bool AsStunt { get => asStunt; set => asStunt = value; }


        public override void ResetObject()
        {
            base.ResetObject();
            AsStunt = false;
        }
    }
}