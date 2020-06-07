using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using TMPro;

namespace CrabMaga
{
    public class MenuChoixUnit : UIMenu
    {

        public CanvasGroup unitTab, leaderTab;
        public Image btnTabUnit, btnTabLeader;
        public Sprite selectedSpriteL, selectedSpriteU, unSelectedSpriteU, unSselectedSpriteL;

        public TextMeshProUGUI tabTitle;
        public LanguageManager languageManager;

        public void LeaderSelection()
        {
            btnTabUnit.GetComponent<Image>().sprite = unSelectedSpriteU;
            unitTab.alpha = 0;
            unitTab.interactable = false;
            unitTab.blocksRaycasts = false;

            btnTabLeader.GetComponent<Image>().sprite = selectedSpriteL;
            leaderTab.alpha = 1;
            leaderTab.interactable = true;
            leaderTab.blocksRaycasts = true;

            switch (languageManager.LanguageEnum)
            {
                case LanguageEnum.Francais:
                    tabTitle.text = "Unites disponibles";
                    break;
                case LanguageEnum.Anglais:
                    tabTitle.text = "Units available";
                    break;
                case LanguageEnum.Crab:
                    break;
            }
        }

        public void UnitsSelection()
        {
            btnTabLeader.GetComponent<Image>().sprite = unSselectedSpriteL;
            leaderTab.alpha = 0;
            leaderTab.interactable = false;
            leaderTab.blocksRaycasts = false;

            btnTabUnit.GetComponent<Image>().sprite = selectedSpriteU;
            unitTab.alpha = 1;
            unitTab.interactable = true;
            unitTab.blocksRaycasts = true;

            switch (languageManager.LanguageEnum)
            {
                case LanguageEnum.Francais:
                    tabTitle.text = "Generaux disponibles";
                    break;
                case LanguageEnum.Anglais:
                    tabTitle.text = "Leaders available";
                    break;
                case LanguageEnum.Crab:
                    break;
            }
        }
    }
}