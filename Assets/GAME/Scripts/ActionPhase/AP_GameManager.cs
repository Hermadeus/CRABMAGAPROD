using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class AP_GameManager : MonoBehaviour, IWinableAction, ILosableAction
    {
        public LevelData levelData = default;

        public void LoseAction()
        {
            throw new System.NotImplementedException();
        }

        public void WinAction()
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IWinableAction
    {
        void WinAction();
    }

    public interface ILosableAction
    {
        void LoseAction();
    }
}