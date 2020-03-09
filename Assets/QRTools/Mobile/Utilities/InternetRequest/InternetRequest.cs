using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

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
            Debug.Log(netTime + " was response");
        }
    }
}