using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using UnityEngine.SceneManagement;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Managers/Scene Manager")]
    public class SceneManaging : SerializedScriptableObject
    {
        public Dictionary<string, SceneReference> sceneDic = new Dictionary<string, SceneReference>();

        public LanguageManager languageManager;

        public SceneReference GetScene(string key)
        {
            SceneReference s;

            sceneDic.TryGetValue(key, out s);

            return s;
        }

        public void RestartScene()
        {
            languageManager.Observables.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToSagaMap()
        {
            languageManager.Observables.Clear();
            SceneManager.LoadScene(GetScene("sagamap"));
        }
    }
}