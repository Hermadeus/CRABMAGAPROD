using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CrabMaga
{
    public class Castle : MonoBehaviour
    {
        public AP_GameManager APGameManager = default;
        public int crabReach = 0;

        public UnityEvent OnReach = default;

        public void ReachCastle()
        {
            crabReach++;
            OnReach.Invoke();
        }
    }
}