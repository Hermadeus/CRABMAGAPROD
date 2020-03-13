using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.Functions;

using Sirenix.OdinInspector;

using DG.Tweening;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
        [FoldoutGroup("References")]
        public CrabFormation crabFormation;
        [FoldoutGroup("References")]
        public Transform crabsParent = default;
        [FoldoutGroup("References")]
        public Castle castle = default;

        public override int PV
        {
            get
            {
                return base.PV;
            }
            set
            {
                base.PV = value;
            }
        }

        public override int Damage => crabFormation.CountOfEntities();

        protected override void Init()
        {
            base.Init();

            poolManager.PoolCrabsToUnit(this, unitData.startPV);
        }

        protected override void UpdateComportement()
        {
            base.UpdateComportement();

            if (UnitTarget == null)
                transform.DOLookAt(castle.transform.position, 1f).SetEase(Ease.Linear);
        }

        public override void GetDamage(int damage)
        {
            base.GetDamage(damage);
        }
    }
}