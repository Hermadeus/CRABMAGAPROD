using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using QRTools.UI;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class MenuVictory : UIMenu
    {
        [FoldoutGroup("References")]
        public AP_GameManager APGameManager = default;

        [FoldoutGroup("References")]
        [SerializeField] TextMeshProUGUI levelNameText = default;
        [FoldoutGroup("References")]
        [SerializeField] TextMeshProUGUI timerText = default;
        [FoldoutGroup("References")]
        [SerializeField] TextMeshProUGUI unitText = default;
        [FoldoutGroup("References")]
        [SerializeField] TextLocalisation unitTextLocalisation = default;
        [FoldoutGroup("References")]
        [SerializeField] ImageState drapeauImage = default;
        [FoldoutGroup("References")]
        [SerializeField] CanvasGroup buttonsVictory, buttonsLost;

        public override void Init()
        {
            base.Init();
            InitLevelName();
        }

        public override void Show()
        {
            base.Show();
            UpdateInfos();
        }

        void InitLevelName()
        {
            levelNameText.text = APGameManager.levelData.levelName;
        }

        public void UpdateInfos()
        {
            timerText.text = UpdateTimerText();
            unitText.text = UpdateUnitCount();
            UpdateVictoryText(APGameManager.AsWin);
            UpdateImage(APGameManager.AsWin);
            UpdateButtons(APGameManager.AsWin);
        }

        string UpdateUnitCount() => APGameManager.CurrentScore.ToString();

        string UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(APGameManager.AP_Timer / 60);
            int secondes = Mathf.FloorToInt(APGameManager.AP_Timer);
            float milliseconde = (APGameManager.AP_Timer - secondes) * 1000;
            return string.Format("{0}' {1}'' {2}",
                minutes.ToString(),
                secondes.ToString(),
                milliseconde.ToString("000")
                );
        }

        void UpdateVictoryText(bool win)
        {
            if (win)
            {
                unitTextLocalisation.text.textFrancais = "Victoire !";
                unitTextLocalisation.text.textAnglais = "Victory !";
                unitTextLocalisation.text.textCrab = "Crabyyyy !";
            }
            else
            {
                unitTextLocalisation.text.textFrancais = "Défaite.";
                unitTextLocalisation.text.textAnglais = "Lost.";
                unitTextLocalisation.text.textCrab = "Crablose.";
            }

            unitTextLocalisation.Notify();
        }

        void UpdateImage(bool win)
        {
            if (win)
                drapeauImage.UpdateImage("win");
            else
                drapeauImage.UpdateImage("lose");
        }

        void UpdateButtons(bool win)
        {
            if (win)
            {
                buttonsVictory.alpha = 1;
                buttonsVictory.blocksRaycasts = true;
                buttonsVictory.interactable = true;
            }
            else
            {
                buttonsLost.alpha = 1;
                buttonsLost.blocksRaycasts = true;
                buttonsLost.interactable = true;
            }
        }
    }
}