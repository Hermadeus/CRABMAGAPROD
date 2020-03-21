using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Level Data")]
    public class LevelData : ScriptableObject
    {
        [BoxGroup("Level Information")]
        public string levelName = "";

        [BoxGroup("Level Rules")]
        public int maxCrab = 3;
    }
}