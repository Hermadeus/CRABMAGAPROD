using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using QRTools.Inputs;

namespace CrabMaga
{
    public class CastleSagamap : MonoBehaviour, Iinteractable
    {
        public LevelData levelData = default;

        public ButtonLevel buttonLevel = default;

        public PlayButtonSG playButtonSG = default;

        private void Awake()
        {
            playButtonSG = FindObjectOfType<PlayButtonSG>();
        }

        public void ChargeLevel()
        {
            FindObjectOfType<MenuChoixUnit>().Show();
            playButtonSG.button.onClick.AddListener(LoadLevel);
        }

        public void LoadLevel()
        {
            SceneManager.LoadScene(levelData.sceneLevel);

        }

        public void Deselect()
        {
            buttonLevel.Hide();
        }

        public void Select()
        {
            buttonLevel.Show();
        }
    }
}