using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Attack/Null Attack")]
    public class NullAttack : BaseAttack
    {
        public override void Attack(Unit _unit, IAttackReceiver _receiver)
        {
            
        }
    }
}