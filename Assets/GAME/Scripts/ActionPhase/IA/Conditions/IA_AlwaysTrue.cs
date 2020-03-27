using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Conditions/Always True")]
    public class IA_AlwaysTrue : IA_Condition
    {
        public override bool Condition(IA_Manager manager)
        {
            return true;
        }
    }
}