using System.Collections;
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

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        public void Win()
        {
            Debug.Log("WIN");
        }
    }
}