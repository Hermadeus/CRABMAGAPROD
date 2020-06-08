using System.Collections;
using System.Collections.Generic;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;
using UnityEngine.Android;
using QRTools.Mobile;
using System;

namespace QRTools.Mobile
{
    [CreateAssetMenu(menuName = "QRTools/Mobile/NotificationsManager")]
    public class NotificationsManager : ScriptableObject
    {
#if UNITY_ANDROID
        public InternetRequest internetRequest = default;
#endif

        public IEnumerator TestNotif()
        {
#if UNITY_ANDROID
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);

            var notification = new AndroidNotification();
            notification.Title = "BWAAAAARG!!";
            notification.Text = "Ce n'est rien, Crab'zilla vient juste de finir son repas...";
            notification.LargeIcon = "game_icon";

            //MonoBehaviour m = FindObjectOfType<MonoBehaviour>();

            //yield return m.StartCoroutine(internetRequest.getTime());

            //DateTime dt = DateTime.Parse(internetRequest.getCurrentTimeNow());
            DateTime dt = DateTime.Now;

            notification.FireTime = dt.AddHours(4);

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
#endif
            yield break;
        }

        public IEnumerator TestNotif(string title, string text)
        {
#if UNITY_ANDROID
            var c = new AndroidNotificationChannel()
            {
                Id = "channel_id",
                Name = "Default Channel",
                Importance = Importance.High,
                Description = "Generic notifications",
            };
            AndroidNotificationCenter.RegisterNotificationChannel(c);

            var notification = new AndroidNotification();
            notification.Title = title;
            notification.Text = text;
            notification.LargeIcon = "game_icon";

            //MonoBehaviour m = FindObjectOfType<MonoBehaviour>();

            //yield return m.StartCoroutine(internetRequest.getTime());

            //DateTime dt = DateTime.Parse(internetRequest.getCurrentTimeNow());
            DateTime dt = DateTime.Now;

            notification.FireTime = dt.AddHours(4);

            AndroidNotificationCenter.SendNotification(notification, "channel_id");
#endif
            yield break;
        }
    }
}