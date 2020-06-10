using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using QRTools.UI;

using Sirenix.OdinInspector;

using DG.Tweening;

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

        public RectTransform ShellIcon;

        public override void Init()
        {
            base.Init();
            InitLevelName();
            ShellIcon.GetComponentInChildren<TextMeshProUGUI>().SetText("+ " + APGameManager.levelData.crabGain.ToString());
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

        public void Chargement()
        {
            FindObjectOfType<EcranChargement>().Show();
        }

        public void UpdateInfos()
        {
            //timerText.text = UpdateTimerText();
            DOTween.To(() => timerText.text, (x) => timerText.text = x, UpdateTimerText(), 1f);

            //unitText.text = UpdateUnitCount();
            DOTween.To(() => unitText.text, (x) => unitText.text = x, UpdateUnitCount(), 1f);

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
        public Sprite VictoireFR, VictoireAng, DefaiteFR, DefaiteAng, VictoireCrab, DefaiteCrab;

        public Image back;
        public Sprite winBack, loseBack;

        void UpdateVictoryText(bool win)
        {
            if (win)
            {
                back.sprite = winBack;

                switch (languageManager.LanguageEnum)
                {
                    case LanguageEnum.Francais:
                        defText.sprite = VictoireFR;
                        break;
                    case LanguageEnum.Anglais:
                        defText.sprite = VictoireAng;

                        break;
                    case LanguageEnum.Crab:
                        defText.sprite = VictoireCrab;
                        break;
                }
            }
            else
            {
                back.sprite = loseBack;

                switch (languageManager.LanguageEnum)
                {
                    case LanguageEnum.Francais:
                        defText.sprite = DefaiteFR;
                        break;
                    case LanguageEnum.Anglais:
                        defText.sprite = DefaiteAng;

                        break;
                    case LanguageEnum.Crab:
                        defText.sprite = DefaiteCrab;
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

        public void GainShellAnim()
        {
            ShellIcon.DOMoveY(ShellIcon.position.y + 150f, 2f);
            ShellIcon.GetComponent<CanvasGroup>().DOFade(0f, .5f);
        }
    }
}