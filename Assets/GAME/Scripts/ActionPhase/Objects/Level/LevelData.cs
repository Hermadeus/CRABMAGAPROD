using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Utilities;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Level/Level Data")]
    public class LevelData : ScriptableObject, IResetable
    {
        public string nameLevel = "";
        public int bestScore = 0;
        public int scoreToReach = 5;

        [Button]
        public void ResetObject()
        {
            bestScore = 0;
        }
    }
}