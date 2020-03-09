using System.Linq;
using UnityEngine;

namespace QRTools.Utilities.Singleton
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;
        private static bool _instantiated;
        public static T Instance
        {
            get
            {
                if (_instantiated) return _instance;
                var singletonName = typeof(T).Name;
                var assets = Resources.LoadAll<T>("");
                if (assets.Length > 1) Debug.LogError("Found multiple " + singletonName + "s on the resources folder. It is a Singleton ScriptableObject, there should only be one.");
                if (assets.Length == 0)
                {
                    _instance = CreateInstance<T>();
                    Debug.LogError("Could not find a " + singletonName + " on the resources folder. It was created at runtime, therefore it will not be visible on the assets folder and it will not persist.");
                }
                else _instance = assets[0];
                _instantiated = true;                
                return _instance;
            }
        }
    }
}
