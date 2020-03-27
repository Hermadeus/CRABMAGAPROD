using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/Test")]
    public class IA_InitBehaviourTest : IA_Behaviour
    {


        public override void CallEvent(IA_Manager manager)
        {
            base.CallEvent(manager);
            //manager.poolingManager.PoolEntity(data.unitType.GetType(), manager.APgameManager.castle.transform.position);

        }
    }
}