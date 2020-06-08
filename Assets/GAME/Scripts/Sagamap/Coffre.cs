using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.Variables;
using TMPro;
using QRTools.Inputs;
using QRTools.Audio;
using DG.Tweening;
using QRTools.UI;
using Sirenix.OdinInspector;
using System;
using Random = UnityEngine.Random;

namespace CrabMaga
{
    public class Coffre : MonoBehaviour, Iinteractable
    {
        public float startTimer = 5;
        public float timer = 0; //28 800

        //public BoolVariable isStart;

        bool isOpen = false;

        public InputTouch coffreTouch;

        public int populationcount;
        public HeaderMoney headerMoney;

        public TextMeshProUGUI timerTxt, populationTxt;

        public AudioEvent son;
        public AudioSource source;

        public PlayerData playerData;

        public UIMenu menuAcc;
        public TextMeshProUGUI textAchat, cost;
        public Button btnAchatRapide;

        public DateTime last_dt;

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            if (!isOpen)
            {
                if (PlayerPrefs.GetFloat("coffre") == 0)
                {
                    PlayerPrefs.SetFloat("coffre", 1);
                    timer = startTimer;
                    last_dt = DateTime.Now;
                    PlayerPrefs.SetInt("h", DateTime.Now.Hour);
                    PlayerPrefs.SetInt("m", DateTime.Now.Minute);
                    PlayerPrefs.SetInt("s", DateTime.Now.Second);
                }
                else
                {
                    int h = PlayerPrefs.GetInt("h");
                    int m = PlayerPrefs.GetInt("m");
                    int s = PlayerPrefs.GetInt("s");

                    timer = startTimer - ((DateTime.Now.Hour - h) * 60 + (DateTime.Now.Minute - m) * 60 + (DateTime.Now.Second - s));
                }
            }

            populationcount = Mathf.RoundToInt(Random.Range(playerData.maxCrab * 0.1f, playerData.maxCrab * .5f));
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                int h = (int)timer / 3600;
                int min = ((int)timer % 3600) / 60;
                int sec = (int)timer % 60;

                timerTxt.text =  h + "h " +  min + "min " + sec + "s";
            }
            else
            {
                timerTxt.text = "Tap to gain " + populationcount + " crabs!";
            }

            if(coffreTouch.objectHit != null)
            {
                Coffre c = coffreTouch.objectHit.GetComponent<Coffre>();
                c.Select();
            }
        }

        public void Select()
        {
            if (timer <= 0 && !isOpen)
            {
                isOpen = true;
                Open();
            }
            else
            {
                OpenAndAccelerate();
            }
        }

        int costAchatRapide;

        public LanguageManager languageManager;

        public void OpenAndAccelerate()
        {
            menuAcc.Show();
            costAchatRapide = (int)timer / 100;

            btnAchatRapide.onClick.AddListener(Accelerate);
            cost.text = costAchatRapide.ToString();

            switch (languageManager.LanguageEnum)
            {
                case LanguageEnum.Francais:
                    textAchat.text = "Ouvre ce coffre pour " + costAchatRapide + " perles.";
                    break;
                case LanguageEnum.Anglais:
                    textAchat.text = "Open this chest now for " + costAchatRapide + " pearls.";
                    break;
                case LanguageEnum.Crab:
                    textAchat.text = "Crab crab craaab cr " + costAchatRapide + " crabs.";
                    break;
            }
        }

        void Accelerate()
        {
            if(playerData.pearlMoney - costAchatRapide < 0)
            {
                menuAcc.rectTransform.DOShakePosition(.5f, 50);
            }
            else
            {
                headerMoney.RemovePearl(costAchatRapide);
                Open();
            }
        }

        public void Open()
        {
            Debug.Log("OPEN LE COFFRE");
            headerMoney.AddCrab(populationcount);


            //transform.DOShakeRotation(1f).SetEase(Ease.InOutSine);
            //GetComponentInChildren<Transform>().DOLocalMoveY(GetComponentInChildren<Transform>().localPosition.y + 5, 2f).SetEase(Ease.InOutCirc);
            //transform.DOScale(Vector3.zero, 5f).SetEase(Ease.OutBack);
            //son.Play(source);

            menuAcc.Hide();

            this.transform.DOScale(Vector3.zero, 2f).SetEase(Ease.InOutSine);
        }

        public void Deselect()
        {

        }

        [Button]
        public void InitPlayerPrefs()
        {
            PlayerPrefs.SetInt("coffre", 0);
        }
    }
}