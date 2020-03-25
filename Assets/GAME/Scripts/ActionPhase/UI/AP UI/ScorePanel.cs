using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using QRTools.UI;

namespace CrabMaga
{
    public class ScorePanel : UIElement
    {
        public AP_GameManager AP_GameManager = default;
        public TextMeshProUGUI scoreText = default;

        public override void Init()
        {
            base.Init();

            UpdateScore();
        }

        public void UpdateScore()
        {
            scoreText.text =
                "Score: " +
                AP_GameManager.CurrentScore.ToString() +
                " / " +
                AP_GameManager.levelData.scoreToReach.ToString();
        }
    }
}