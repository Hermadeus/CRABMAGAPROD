using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;

namespace QRTools.Variables
{
    public class DictionaryVariable<TKey, TValue> : SerializedScriptableObject
    {
        [SerializeField, TextArea(3,5)] private string description;

        public Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

        public TValue GetValue(TKey key)
        {
            TValue obj;
            
            dict.TryGetValue(key, out obj);

            return obj;
        }

        public TValue[] GetValues(params TKey[] key)
        {
            TValue[] obj = new TValue[key.Length];

            for (int i = 0; i < key.Length; i++)
                obj.Append(GetValue(key[i]));

            return obj;
        }

        public bool ValueExist(TKey key)
        {
            TValue obj;
            dict.TryGetValue(key, out obj);

            if (obj != null)
                return true;
            else
                return false;
        }

        public bool ValuesExist(params TKey[] key)
        {
            TValue[] obj = new TValue[key.Length];

            for (int i = 0; i < key.Length; i++)
                obj = GetValues(key);

            if (obj.Length == key.Length)
                return true;
            else
                return false;
        }
    }    
}
