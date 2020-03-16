using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CrabMaga
{
    public class AP_GameManager : MonoBehaviour, IWinableAction, ILosableAction
    {
        public LevelData levelData = default;
        public Castle castle = default;

        public List<Crab> crabsInvoke = new List<Crab>();

        private void Update()
        {
            if (WinCondition() == true)
                OnWinAction();

            else if (LoseCondition() == true)
                OnLoseAction();
        }

        public bool WinCondition()
        {
            if (castle.crabReach >= levelData.scoreToReach)
                return true;

            return false;
        }

        public bool LoseCondition()
        {
            if (crabsInvoke.Count == 0)
                return true;

            return false;
        }

        public void OnWinAction()
        {
            Debug.Log("is Win");

            throw new System.NotImplementedException();
        }

        public void OnLoseAction()
        {
            Debug.Log("is Lose");

            throw new System.NotImplementedException();
        }
    }

    public interface IWinableAction
    {
        void OnWinAction();
    }

    public interface ILosableAction
    {
        void OnLoseAction();
    }
}