using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/2H")]

    public class IA_2H : IA_Behaviour
    {
        public IA_XCounterOnLine IA_EnemyOfThisLine = default;


        public override void CallEvent(IA_Manager manager)
        {
            base.CallEvent(manager);

            IA_EnemyOfThisLine.Instantiation(manager);
        }
    }
}