using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Net;
using System.Net.Sockets;

namespace QRTools.Mobile
{
    [CreateAssetMenu(menuName = "QRTools/Mobile/InternetRequest")]
    public class InternetRequest : ScriptableObject
    {
        public IEnumerator GetInternetTime()
        {
            UnityWebRequest myHttpWebRequest = UnityWebRequest.Get("http://www.microsoft.com");
            yield return myHttpWebRequest.Send();

            string netTime = myHttpWebRequest.GetResponseHeader("date");
            DateTime dt = DateTime.Parse(netTime);
            Debug.Log(dt.ToString() + " was response");

            yield return dt;
        }

        private string _url = "http:\\Path_to_php";
        private string _timeData;
        private string _currentTime;
        private string _currentDate;

        public IEnumerator getTime()
        {
            UnityWebRequest myHttpWebRequest = UnityWebRequest.Get(_url);
            yield return myHttpWebRequest.Send();

            _timeData = myHttpWebRequest.GetResponseHeader("date");
            string[] words = _timeData.Split('/');

            Debug.Log("The date is : " + words[0]);
            Debug.Log("The time is : " + words[1]);

            _currentDate = words[0];
            _currentTime = words[1];
        }

        //get the current date
        public string getCurrentDateNow()
        {
            return _currentDate;
        }


        //get the current Time
        public string getCurrentTimeNow()
        {
            return _currentTime;
        }

        
    }
}