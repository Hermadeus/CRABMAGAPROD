using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class AP_GameManager : MonoBehaviour
    {
        public LevelData levelData = default;

        [BoxGroup("AP Informations")] 
        public List<CrabFormation> crabFormationOnBattle = new List<CrabFormation>();
        [BoxGroup("AP Informations")]
        public List<CrabUnit> crabUnitOnBattle = new List<CrabUnit>();

        [BoxGroup("AP Informations")]
        public Leader leaderOnBattle;

        [BoxGroup("AP Informations")]
        public List<Enemy> enemiesOnBattle = new List<Enemy>();

        [BoxGroup("AP Informations")]
        [SerializeField] int currentScore = 0;
        public int CurrentScore
        {
            get => currentScore;
            set
            {
                currentScore = value;
                scorePanel.UpdateScore();

                if (value >= levelData.scoreToReach)
                    Win();
            }
        }

        [BoxGroup("References")]
        public Castle castle = default;
        [BoxGroup("References")]
        public ScorePanel scorePanel = default;

        public UnityEvent
            OnWinEvent = new UnityEvent(),
            OnLoseEvent = new UnityEvent();

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

        bool asFinish = false;
        public bool AsFinish
        {
            get => asFinish;
            set
            {
                asFinish = value;
            }
        }

        bool asWin = false;
        public bool AsWin
        {
            get => asWin;
            set
            {
                asWin = value;
            }
        }

        public float AP_Timer = 0f;

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            Timer();
        }

        void Timer()
        {
            if (inPause && AsFinish) return;

            AP_Timer += Time.deltaTime;
        }

        void Init()
        {
            Application.targetFrameRate = 60;
            Time.timeScale = 1f;
        }

        public void Win()
        {
            AsFinish = true;
            AsWin = true;

            OnWinEvent.Invoke();
        }

        public void Lose()
        {
            AsFinish = true;
            AsWin = false;

            OnLoseEvent.Invoke();
        }
    }
}