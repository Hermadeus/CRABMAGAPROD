using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
        public List<CrabGroup> crabGroup = new List<CrabGroup>();

        public override float Damage
        {
            get
            {
                damage = GetDamageTotal();
                return damage;
            }
            set
            {
                base.Damage = value;
            }
        }

        public override void Init()
        {
            base.Init();

            InitCrabs();

            Damage = GetDamageTotal();
        }

        void InitCrabs()
        {
            for (int i = 0; i < crabGroup.Count; i++)
                crabGroup[i].crab.crabUnitReference = this;
        }

        public override void AsWin()
        {
            base.AsWin();
            MovementBehaviourEnum = MovementBehaviourEnum.JOIN_CASTLE_MOVEMENT;
        }

        public float GetDamageTotal()
        {
            float d = 0;

            for (int i = 0; i < crabGroup.Count; i++)
            {
                if (crabGroup[i].crab != null)
                    d += crabGroup[i].crab.Damage;
            }

            return d;
        }
    }

    [System.Serializable]
    public class CrabGroup
    {
        public Crab crab = default;
        public Vector3 relativePosition = new Vector3();
    }
}