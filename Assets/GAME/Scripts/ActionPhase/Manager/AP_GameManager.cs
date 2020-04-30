using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using QRTools.Audio;
using QRTools.Inputs;

using TMPro;

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
                //scorePanel.UpdateScore();
                castle.healthSlider.value = value;

                Debug.Log("CURRENT SCORE = " + value);

                if (value == Mathf.CeilToInt(levelData.scoreToReach / 2))
                {
                    IA_Manager.onCastleReachMiddlePV.Invoke(IA_Manager);
                    castle.onMiddlePV.Invoke();
                }

                if (value == Mathf.CeilToInt(levelData.scoreToReach - (levelData.scoreToReach / 4)))
                {
                    IA_Manager.onCastleReachMiddlePV.Invoke(IA_Manager);
                    castle.onQuartPV.Invoke();
                    Debug.Log("IL RESTE UN QUART DES PV");
                }

                if (value >= levelData.scoreToReach)
                    Win();
            }
        }

        [BoxGroup("AP Informations")]
        public float AP_Timer = 0f;

        [BoxGroup("References")]
        public Castle castle = default;
        //[BoxGroup("References")]
        //public ScorePanel scorePanel = default;
        [BoxGroup("References")]
        public GuardHouseManager guardHouseManager = default;
        [BoxGroup("References")]
        public CameraSlider cameraSlider = default;
        [BoxGroup("References")]
        public IA_Manager IA_Manager = default;
        [BoxGroup("References")]
        public InputTouch inputWheel = default;
        

        private int currentUnitCountInt;
        public int CurrentUnitCountInt
        {
            get => currentUnitCountInt;
            set
            {
                currentUnitCountInt = value;
                currentUnitCount.SetText(value.ToString());

                if (value < levelData.maxCrabInSameTime - 2)
                    currentUnitCount.color = Color.white;
                else if (value == levelData.maxCrabInSameTime - 1)
                    currentUnitCount.color = new Color(255, 165, 0);
                else if (value == levelData.maxCrabInSameTime)
                    currentUnitCount.color = Color.red;
            }
        }

        private int totalUnitCountInt;
        public int TotalUnitCountInt
        {
            get => totalUnitCountInt;
            set
            {
                totalUnitCountInt = value;
                totalUnitCount.SetText(value.ToString());

                if (value > 3 && value < levelData.maxCrab)
                    totalUnitCount.color = Color.white;
                else if (value > 0 && value < 3)
                    totalUnitCount.color = new Color(255, 165, 0);
                else if (value == 0)
                {
                    totalUnitCount.color = Color.red;
                    Lose();
                }
            }
        }

        [BoxGroup("References")]
        public TextMeshProUGUI currentUnitCount = default;
        [BoxGroup("References")]
        public TextMeshProUGUI totalUnitCount = default;

        public RappelInput rappelInput = default;

        public UnityEvent
            OnWinEvent = new UnityEvent(),
            OnLoseEvent = new UnityEvent(),
            OnEndEvent = new UnityEvent();

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

            StartCoroutine(RappelInputCor());

            inputWheel.onLongTapEnd.AddListener(StopRappelInput);

            Time.timeScale = 1;
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

            currentUnitCountInt = 0;
            totalUnitCountInt = levelData.maxCrab;
        }

        [Button]
        public void Win()
        {
            AsFinish = true;
            AsWin = true;

            OnEndEvent.Invoke();
            OnEnd();
            OnWinEvent.Invoke();
        }

        [Button]
        public void Lose()
        {
            AsFinish = true;
            AsWin = false;

            OnEndEvent.Invoke();
            OnEnd();
            OnLoseEvent.Invoke();
        }

        void OnEnd()
        {
            Debug.Log("PARTIE FINIE");
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

        IEnumerator RappelInputCor()
        {
            yield return new WaitForSeconds(15);
            rappelInput.Rapelle();
            yield break;
        }

        public void StopRappelInput()
        {
            rappelInput.asStop = true;
            rappelInput.anim.SetTrigger("end");
            StopCoroutine(RappelInputCor());
        }
    }
}