using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace CrabMaga
{
    public class ChargementSG : MonoBehaviour
    {
        public Slider sldr;

        public SceneReference SG;

        private void Awake()
        {
            StartCoroutine(LoadAsync(SG.ScenePath));
        }

        IEnumerator LoadAsync (string sceneIndex)
        {
            yield return new WaitForSeconds(2f);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                sldr.value = progress;

                Debug.Log(operation.progress);
                yield return null;
            }
        }
    }
}