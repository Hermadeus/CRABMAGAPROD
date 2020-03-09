using UnityEngine;
using UnityEngine.Events;

namespace QRTools.Variables
{
    [CreateAssetMenu(menuName = "QRTools/Variables/Dictionaries/Dictionary String-UnityEvent", order = 2)]
    public class DictionaryStringEventVariable : DictionaryVariable<string, UnityEvent>
    {
        public void AddListener(string key, UnityAction action) => GetValue(key).AddListener(action);

        public void AddListeners(string key, params UnityAction[] action)
        {
            for (int i = 0; i < action.Length; i++)
                GetValue(key).AddListener(action[i]);
        }

        public void AddListeners(UnityAction action, params string[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
                GetValue(keys[i]).AddListener(action);
        }

        public void RemoveListener(string key, UnityAction action) => GetValue(key).RemoveListener(action);

        public void RemoveAllListner(string key) => GetValue(key).RemoveAllListeners();

        public void InvokeMultipleEvent(params string[] key)
        {
            for (int i = 0; i < key.Length; i++)
                GetValue(key[i]).Invoke();
        }
    }
}
