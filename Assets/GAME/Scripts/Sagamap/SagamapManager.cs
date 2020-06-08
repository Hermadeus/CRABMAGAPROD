using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Inputs;
using QRTools.Mobile;
using DG.Tweening;
using QRTools.Variables;

using System;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class SagamapManager : MonoBehaviour
    {
        public List<CastleSagamap> castles = new List<CastleSagamap>();

        public NotificationsManager notificationsManager = default;
        public InternetRequest InternetRequest = default;

        public LanguageManager languageManager = default;

        public static SagamapManager instance;

        public GameObject ecranchargement;

        public CoquillageARamasser[] coquillages;

        public HeaderMoney headerMoney;

        public NotificationsManager NotificationsManager;

        private void Awake()
        {
            instance = this;

            //var objs = FindObjectsOfType<CastleSagamap>();
            //for (int i = 0; i < objs.Length; i++)
            //    castles.Add(objs[i]);

            StartCoroutine(notificationsManager.TestNotif());
            //StartCoroutine(InternetRequest.getTime());

            languageManager.UpdateObservable();
            Time.timeScale = 1;
            ecranchargement.SetActive(true);

            headerMoney.Load();

            if (PlayerPrefs.GetInt("fp") == 1)
            {
                int hours = PlayerPrefs.GetInt("lh");
                int min = PlayerPrefs.GetInt("lm");

                int minT = (DateTime.Now.Hour - hours) * 60 + Mathf.Abs(DateTime.Now.Minute - min);

                Debug.Log("MIN : " + minT);

                headerMoney.AddCrab((headerMoney.playerData.maxCrab / 480) * minT);
                
                PlayerPrefs.SetInt("lh", DateTime.Now.Hour);
                PlayerPrefs.SetInt("lm", DateTime.Now.Minute);

                StartCoroutine(notificationsManager.TestNotif(
                    "Ton armée a besoin de toi !" ,
                    "Ta population est au maximum, reviens vite !"));
            }
            else
                PlayerPrefs.SetInt("fp", 1);


            for (int i = 0; i < coquillages.Length; i++)
            {
                int x = Random.Range(0, 100);

                if (x < 50)
                {
                    coquillages[i]?.gameObject.SetActive(true);
                }
                else
                {
                    coquillages[i]?.gameObject.SetActive(false);
                }
            }

            coquillageTouch.onTouchEnter.AddListener(OnCoquillageTouch);
        }

        [Button]
        public void SetPlayerPref()
        {
            PlayerPrefs.SetInt("lh", DateTime.Now.Hour);
            PlayerPrefs.SetInt("lm", DateTime.Now.Minute);

            PlayerPrefs.SetInt("fp", 0);

            PlayerPrefs.SetInt("levelQuatreAnim", 0);
        }

        public InputTouch coquillageTouch;
        public void OnCoquillageTouch()
        {
            if (coquillageTouch.objectHit == null)
                return;

            if (coquillageTouch.objectHit.GetComponent<CoquillageARamasser>())
            {
                CoquillageARamasser c = coquillageTouch.objectHit.GetComponent<CoquillageARamasser>();
                c.Ramasser();
            }
        }

        public CrabUnitData craberserk;
        public PlayerData playerData;
        public BoolVariable firstPartie;
        public TutoSagamap tutoSagamap;

        public void InitLVLOne()
        {
            playerData.entityData_slot01 = craberserk;
            playerData.entityData_slot02 = null;
            playerData.entityData_slot03 = null;
            playerData.entityData_slot04 = null;

            playerData.leader_slot = null;

            if (firstPartie.Value == true)
            {
                //firstPartie.Value = false;

                //playerData.crabMoney = 1000;
                //playerData.shellMoney = 0;
                //playerData.pearlMoney = 0;
            }
        }
    }
}