using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using TMPro;

namespace CrabMaga
{
    public class UIMenuArmyButtonSelection : MonoBehaviour
    {
        public Image back;
        public Sprite selectedSprite, unselectedSprite;

        public StringLanguage panelName;
        public TextMeshProUGUI title;

        public LanguageManager languageManager;

        public void Select()
        {
            back.sprite = selectedSprite;
            title.SetText(panelName.GetCurrentText(languageManager.LanguageEnum));
        }

        public void UnSelect()
        {
            back.sprite = unselectedSprite;
        }
    }
}