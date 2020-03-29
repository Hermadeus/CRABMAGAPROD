using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Sirenix.OdinInspector;

using QRTools.Utilities;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Level Data")]
    public class LevelData : ScriptableObject, IResetable
    {
        [BoxGroup("Level Information")]
        public string levelName = "";
        [BoxGroup("Level Information")]
        public SceneReference sceneLevel = default;

        [BoxGroup("Level Rules")]
        public int maxCrab = 3;
        public int scoreToReach = 9;

        [BoxGroup("Player Achievements")]
        public int bestScore = 0;

        [Button]
        public void ResetObject()
        {
            bestScore = 0;
        }
    }
}