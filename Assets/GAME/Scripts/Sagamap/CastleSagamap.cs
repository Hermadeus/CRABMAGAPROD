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

        public Image s1, s2, s3;
        public Sprite star01, star02, star03;
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
            playButtonSG.button.onClick.AddListener(LoadLevel);
        }

        public void LoadLevel()
        {
            StartCoroutine(LL());
        }

        IEnumerator LL()
        {
            yield return new WaitForSeconds(2f);
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

        public void SetStars()
        {
            if (levelData.star01)
            {
                s1.sprite = star01;
            }

            if (levelData.star02)
            {
                s2.sprite = star02;
            }

            if (levelData.star03)
            {
                s3.sprite = star03;
            }
        }
    }
}