using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/IN")]
    public class IA_IN : IA_Behaviour
    {
        public override void CallEvent(IA_Manager manager)
        {
            base.CallEvent(manager);
            instantiationRule.Instantiation(manager);
        }
    }
}