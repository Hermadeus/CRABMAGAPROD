using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Attack/Attack Crab Unit")]
    public class AttackCrabUnit : SimpleAttack
    {
        public override void Effect(Unit _unit, IAttackReceiver _receiver)
        {
            //Debug.Log("ta");
            
            //CrabUnit crabUnit = _receiver as CrabUnit;

            //Crab c = null;

            //for (int i = 0; i < crabUnit.CrabGroup.Count; i++)
            //{
            //    if (crabUnit.CrabGroup[i].Crab != null)
            //    {
            //        c = crabUnit.CrabGroup[i].Crab;
            //        break;
            //    }
            //}

            //if(c != null)
            //    c.ReceiveAttack(_unit.Damage);

        }
    }
}