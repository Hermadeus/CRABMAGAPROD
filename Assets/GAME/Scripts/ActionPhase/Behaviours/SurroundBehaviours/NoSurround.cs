using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Surround/No Surround")]
    public class NoSurround : ScriptableObject, ISurroundBehaviour
    {
        public void Surround(Unit unit, Transform target)
        {
            unit.asSurround = true;
            unit.HaveReachEnemy();
        }
    }
}