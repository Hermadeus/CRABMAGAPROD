using QRTools.Actions;
using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(menuName = "QRTools/Variables/Dictionaries/Dictionary String-Action", order = 1)]
    public class DictionaryStringActionVariable : DictionaryVariable<string, Action>
    {
        public void PlayAction(string key) => GetValue(key).Execute();

        public bool ActionExist(string key) => ValueExist(key);
    }
}