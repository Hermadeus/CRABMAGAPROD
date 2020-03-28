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
        public Dictionary<string, Scene> sceneDic = new Dictionary<string, Scene>();

        public Scene GetScene(string key)
        {
            Scene s;

            sceneDic.TryGetValue(key, out s);

            return s;
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}