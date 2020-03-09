using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class CrabUnit : Unit
    {
        public CrabFormation crabFormation;

        public Transform crabsParent = default;

        public PoolManager poolManager;

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

        protected override void Init()
        {
            base.Init();

            InitEntities();
        }

        void InitEntities()
        {
               
        }
    }

    
}