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

        public SceneReference sagamap;

        public LevelData[] leveldatas;

        public LanguageManager languageManager;

        public SceneReference GetScene(string key)
        {
            SceneReference s;

            sceneDic.TryGetValue(key, out s);

            return s;
        }

        public void RestartScene()
        {
            EcranChargement ec = FindObjectOfType<EcranChargement>();
            Time.timeScale = 1;
            FindObjectOfType<EcranChargement>().Show();
            ec.StartCoroutine(RS());
        }

        IEnumerator RS()
        {
            yield return new WaitForSeconds(2f);
            languageManager.Observables.Clear();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            yield break;
        }

        public void ReturnToSagaMap()
        {
            Time.timeScale = 1;
            EcranChargement ec = FindObjectOfType<EcranChargement>();
            ec.StartCoroutine(SM());
        }

        IEnumerator SM()
        {
            yield return new WaitForSeconds(.1f);
            languageManager.Observables.Clear();
            SceneManager.LoadScene(sagamap.ScenePath);
            yield break;
        }

        public void NextLevel()
        {
            Debug.Log("GO LEVEL : " + FindObjectOfType<AP_GameManager>().nextLevel.levelName);
            SceneManager.LoadScene(FindObjectOfType<AP_GameManager>().nextLevel.sceneLevel);

            //int index = FindObjectOfType<AP_GameManager>().levelData.LevelIndex;

            //for (int i = 0; i < leveldatas.Length; i++)
            //{
            //    if (leveldatas[i].LevelIndex == i + 1)
            //    {
            //        Debug.Log("GO LEVEL : " + leveldatas[i].sceneLevel);
            //        SceneManager.LoadScene(leveldatas[i].sceneLevel);
            //    }
            //}
        }

        public LevelData GetNextLevelData(AP_GameManager gm)
        {
            int index = gm.levelData.LevelIndex;

            leveldatas[index].isLock = false;

            return null;
        }
    }
}