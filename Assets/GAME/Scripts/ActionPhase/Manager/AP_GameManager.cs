using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using QRTools.Audio;

namespace CrabMaga
{
    public class AP_GameManager : MonoBehaviour
    {
        public LevelData levelData = default;

        [BoxGroup("AP Informations"), ReadOnly] 
        public List<CrabFormation> crabFormationOnBattle = new List<CrabFormation>();
        [BoxGroup("AP Informations"), ReadOnly]
        public List<CrabUnit> crabUnitOnBattle = new List<CrabUnit>();

        [BoxGroup("AP Informations"), ReadOnly]
        public Leader leaderOnBattle;

        [BoxGroup("AP Informations"), ReadOnly]
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
                castle.healthSlider.value = value;

                if (value >= levelData.scoreToReach)
                    Win();
            }
        }

        [BoxGroup("AP Informations")]
        public float AP_Timer = 0f;

        [BoxGroup("References")]
        public Castle castle = default;
        [BoxGroup("References")]
        public ScorePanel scorePanel = default;
        [BoxGroup("References")]
        public GuardHouseManager guardHouseManager = default;
        [BoxGroup("References")]
        public CameraSlider cameraSlider = default;

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

        public InstantiationZone CurrentInstantiationZone = default;

        [BoxGroup("Audio")]
        public AudioSource audioSource = default;
        [BoxGroup("Audio")]
        public SimpleAudioEvent winSound = default;
        [BoxGroup("Audio")]
        public SimpleAudioEvent loseSound = default;

        private void Awake()
        {
            Init();

            OnLoseEvent.AddListener(OnLose);
            OnWinEvent.AddListener(OnWin);
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

        public CrabFormation GetFormationWithHighterCrabs()
        {
            float x = 0f;
            CrabFormation crabFormation = null;

            if (crabFormationOnBattle.Count == 0)
                return null;

            for (int i = 0; i < crabFormationOnBattle.Count; i++)
                if(crabFormationOnBattle[i].CrabUnits.Count > x)
                    crabFormation = crabFormationOnBattle[i];

            return crabFormation;
        }

        public void OnWin()
        {
            for (int i = 0; i < crabUnitOnBattle.Count; i++)
                crabUnitOnBattle[i].OnWin();

            for (int i = 0; i < enemiesOnBattle.Count; i++)
                enemiesOnBattle[i].OnLose();

            winSound.Play(audioSource);
        }

        public void OnLose()
        {
            for (int i = 0; i < crabUnitOnBattle.Count; i++)
                crabUnitOnBattle[i].OnLose();

            for (int i = 0; i < enemiesOnBattle.Count; i++)
                enemiesOnBattle[i].OnWin();

            loseSound.Play(audioSource);
        }
    }
}