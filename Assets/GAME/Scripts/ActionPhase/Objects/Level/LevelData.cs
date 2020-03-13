using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Utilities;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Level/Level Data")]
    public class LevelData : ScriptableObject, IResetable, IWinableAction, ILosableAction
    {
        public string nameLevel = "";
        public int score = 0;
        public int scoreToReach = 5;

        [Button]
        public void ResetObject()
        {
            score = 0;
        }

        public bool IsWin()
        {
            if (score >= scoreToReach)
                return true;

            return false;
        }

        public void WinAction()
        {
            throw new System.NotImplementedException();
        }

        public void LoseAction()
        {
            throw new System.NotImplementedException();
        }
    }
}