﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class AP_GameManager : MonoBehaviour
    {
        public LevelData levelData = default;

        [BoxGroup("AP Informations")] 
        public List<CrabFormation> crabFormationOnBattle = new List<CrabFormation>();

        [BoxGroup("AP Informations")]
        [SerializeField] int currentScore = 0;
        public int CurrentScore
        {
            get => currentScore;
            set
            {
                currentScore = value;

                if (value >= levelData.scoreToReach)
                    Win();
            }
        }

        [BoxGroup("References")]
        public Castle castle = default;

        bool inPause = false;
        public bool InPause
        {
            get => inPause;
            set
            {
                inPause = value;

                if (value)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
            }
        }

        private void Awake()
        {
            Application.targetFrameRate = 60;
            Time.timeScale = 1f;
        }

        public void Win()
        {
            Debug.Log("WIN");
        }
    }
}