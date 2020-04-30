using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Sirenix.OdinInspector;

using QRTools.Utilities;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Level Data")]
    public class LevelData : SerializedScriptableObject, IResetable, ISavable
    {
        [BoxGroup("Level Information")]
        public TextLanguage levelName;
        [BoxGroup("Level Information")]
        public SceneReference sceneLevel = default;

        public int LevelIndex = 1;

        [BoxGroup("Level Rules")]
        public int 
            maxCrabInSameTime = 3,
            maxCrab = 10,
            scoreToReach = 9;

        [BoxGroup("Player Achievements")]
        public int bestScore = 0;

        public StarWinCondition cond01, cond02, cond03;
        public bool star01, star02, star03;

        public bool asWin = false;

        public void Load()
        {
            bestScore = PlayerPrefs.GetInt(levelName + "score");
        }

        [Button]
        public void ResetObject()
        {
            bestScore = 0;
            star01 = false;
            star02 = false;
            star03 = false;
            asWin = false;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(levelName + "score", bestScore);
        }

        public void TestStars(AP_GameManager gm)
        {
            if (cond01.WinStar(gm) == true)
            {
                star01 = true;
            }

            if (cond02.WinStar(gm) == true)
            {
                star02 = true;
            }

            if (cond03.WinStar(gm) == true)
            {
                star03 = true;
            }
        }
    }
}