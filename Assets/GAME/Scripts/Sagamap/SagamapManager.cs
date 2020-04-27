using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Inputs;
using QRTools.Mobile;

namespace CrabMaga
{
    public class SagamapManager : MonoBehaviour
    {
        public List<CastleSagamap> castles = new List<CastleSagamap>();

        public NotificationsManager notificationsManager = default;
        public InternetRequest InternetRequest = default;

        public LanguageManager languageManager = default;

        public static SagamapManager instance;


        private void Awake()
        {
            instance = this;

            var objs = FindObjectsOfType<CastleSagamap>();
            for (int i = 0; i < objs.Length; i++)
                castles.Add(objs[i]);

            StartCoroutine(notificationsManager.TestNotif());
            //StartCoroutine(InternetRequest.getTime());

            languageManager.UpdateObservable();
            Time.timeScale = 1;
        }
    }
}