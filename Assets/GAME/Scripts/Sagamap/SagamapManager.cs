using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Inputs;
using QRTools.Mobile;
using DG.Tweening;

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

            for (int i = 0; i < coquillages.Length; i++)
            {
                int x = Random.Range(0, 100);

                if(x < 50)
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
    }
}