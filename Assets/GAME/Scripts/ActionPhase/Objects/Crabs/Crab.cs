using QRTools.Functions;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class Crab : Unit
    {
        [FoldoutGroup("References")]
        public Castle castle = default;

        protected override void UpdateComportement()
        {
            base.UpdateComportement();

            FunctionsUseful.DistanceMinAction(transform, castle.transform, .2f, ReachCastle);
        }

        public void ReachCastle()
        {
            castle.ReachCastle();
            Push();
        }
    }
}
