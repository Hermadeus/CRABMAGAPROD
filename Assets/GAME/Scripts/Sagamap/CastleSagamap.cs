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

        public void ChargeLevel()
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