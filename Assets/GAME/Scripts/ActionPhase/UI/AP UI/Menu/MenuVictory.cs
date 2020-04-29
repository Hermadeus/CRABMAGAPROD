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
        [FoldoutGroup("References")]
        [SerializeField] LanguageManager languageManager = default;

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
            levelNameText.text = APGameManager.levelData.levelName.GetCurrentText(languageManager.LanguageEnum);
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

        public Image defText;
        public Sprite VictoireFR, VictoireAng, DefaiteFR, DefaiteAng;

        void UpdateVictoryText(bool win)
        {
            if (win)
            {
                switch (languageManager.LanguageEnum)
                {
                    case LanguageEnum.Francais:
                        defText.sprite = VictoireFR;
                        break;
                    case LanguageEnum.Anglais:
                        defText.sprite = VictoireAng;

                        break;
                    case LanguageEnum.Crab:
                        break;
                }
            }
            else
            {
                switch (languageManager.LanguageEnum)
                {
                    case LanguageEnum.Francais:
                        defText.sprite = DefaiteFR;
                        break;
                    case LanguageEnum.Anglais:
                        defText.sprite = DefaiteAng;

                        break;
                    case LanguageEnum.Crab:
                        break;
                }
            }
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