using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using Sirenix.OdinInspector;

using QRTools.Utilities;
using QRTools.Variables;

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
            scoreToReach = 9,
            castleAllyHealth;

        [BoxGroup("Player Achievements")]
        public int bestScore = 0;

        public StarWinCondition cond01, cond02, cond03;
        public bool star01, star02, star03;

        public bool asWin = false;

        public bool isLock = true;
        public bool levelTuto = true;

        public EntityData entity_unlock = default;

        public int crabGain, pearlGain, shellGain, maxCrabSup;

        public Vector2IntVariable XP;

        public PlayerData playerData;

        public bool lvlInfini = false;

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
            //PlayerPrefs.SetInt(levelName + "score", bestScore);
        }

        public void TestStars(AP_GameManager gm)
        {
            int xp = 0;

            if (cond01.WinStar(gm) == true)
            {
                star01 = true;
                xp++;
            }

            if (cond02.WinStar(gm) == true)
            {
                star02 = true;
                xp++;
            }

            if (cond03.WinStar(gm) == true)
            {
                star03 = true;
                xp++;
            }

            if (!asWin)
            {
                //XP.SetValueX(XP.GetValueX() + xp);

                int xpMore = PlayerPrefs.GetInt("xpPlayer") + xp;
                PlayerPrefs.SetInt("xpPlayer", xp);

                if(LevelIndex % 4 == 0) 
                    playerData.maxCrab += maxCrabSup;
            }

            if (lvlInfini)
            {
                LevelIndex++;
                levelName.textFrancais = "Niveau " + LevelIndex.ToString();
                levelName.textAnglais = "Level " + LevelIndex.ToString();
                levelName.textCrab = "Crabel " + LevelIndex.ToString();
            }
        }

        public EntityData crablinde;
        public EntityData necrabancien;

        public void TestLevelInfini()
        {
            if (LevelIndex % 5 == 0) castleAllyHealth++;

            if (LevelIndex == 10)
                crablinde.isLock = false;

            if (LevelIndex == 12)
                necrabancien.isLock = false;

            LevelIndex++;
            scoreToReach++;
        }
    }
}   