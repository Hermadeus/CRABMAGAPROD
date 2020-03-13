using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Death/Simple Death")]
    public class SimpleDeath : ScriptableObject, IDieBehaviour
    {
        public void Die(Unit unit)
        {
            unit.Push();
        }
    }
}