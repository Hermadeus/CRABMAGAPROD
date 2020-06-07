using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

using QRTools.Inputs;
using UnityEngine.UI;

namespace CrabMaga
{
    public class CastleSagamap : MonoBehaviour, Iinteractable
    {
        public LevelData levelData = default;

        public ButtonLevel buttonLevel = default;

        public PlayButtonSG playButtonSG = default;

        public GameObject s1, s2, s3;
        public GameObject[] elementsAnnexes = default;

        public float posRotCamera;

        public bool SkipArmyChoiceMenu = false;

        public UnityEvent tutoEvent;

        private void Awake()
        {
            playButtonSG = FindObjectOfType<PlayButtonSG>();
            SetStars();

            if (levelData.asWin)
            {
                for (int i = 0; i < elementsAnnexes.Length; i++)
                {
                    elementsAnnexes[i].SetActive(true);
                }
            }
            else
            {
                for (int i = 0; i < elementsAnnexes.Length; i++)
                {
                    elementsAnnexes[i].SetActive(false);
                }
            }

            buttonLevel.isLock = levelData.isLock;
        }

        public void ChargeLevel()
        {
            if(SkipArmyChoiceMenu == true)
            {
                FindObjectOfType<EcranChargement>().Show();

                tutoEvent?.Invoke();

                LoadLevel();
                return;
            }

            FindObjectOfType<MenuChoixUnit>().Show();

            Debug.Log(FindObjectOfType<PlayButtonSG>().name);

            FindObjectOfType<PlayButtonSG>().button.onClick.AddListener(LoadLevel);
        }

        public void LoadLevel()
        {
            Invoke("ChargeLVL", 1f);
        }

        public void ChargeLVL()
        {
            SceneManager.LoadScene(levelData.sceneLevel);
        }

        IEnumerator LL()
        {
            yield return new WaitForSeconds(.1f);
            SceneManager.LoadScene(levelData.sceneLevel);

            yield break;
        }

        public void Deselect()
        {
            buttonLevel.Hide();
        }

        public void Select()
        {
            buttonLevel.Show();
        }

        void SetOff()
        {
            buttonLevel.gameObject.SetActive(false);
        }

        void SetOn()
        {
            buttonLevel.gameObject.SetActive(true);
        }

        public void SetStars()
        {
            if (levelData.star01)
            {
                s1.SetActive(true);
            }

            if (levelData.star02)
            {
                s2.SetActive(true);
            }

            if (levelData.star03)
            {
                s3.SetActive(true);
            }
        }
    }
}