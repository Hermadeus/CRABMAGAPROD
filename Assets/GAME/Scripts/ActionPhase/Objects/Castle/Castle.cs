using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CrabMaga
{
    public class Castle : MonoBehaviour
    {
        public AP_GameManager APGameManager = default;

        public UnityEvent OnReach = default;

        public void ReachCastle()
        {
            APGameManager.levelData.score++;
            OnReach.Invoke();
        }
    }
}