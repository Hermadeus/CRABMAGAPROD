using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Unit/Unit Data")]
    public class UnitData : ScriptableObject
    {
        public string unitName = default;

        public int startPV = 15;
        public int attackPower = 1;

        public float attackSpeed = 2f;
        public float attackRange = .5f;
    }
}