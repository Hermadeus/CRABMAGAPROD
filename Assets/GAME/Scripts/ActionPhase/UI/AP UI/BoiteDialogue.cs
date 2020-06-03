using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

using TMPro;

namespace CrabMaga
{
    public class BoiteDialogue : UIMenu
    {
        public LanguageManager languageManager;

        public TextMeshProUGUI dialogue;

        public Sprite defaultIcon;
        public Image icon;

        public void ShowDialogue(string fr, string ang, Sprite _icon)
        {
            switch (languageManager.LanguageEnum)
            {
                case LanguageEnum.Francais:
                    dialogue.text = fr;
                    break;
                case LanguageEnum.Anglais:
                    dialogue.text = ang;
                    break;
                case LanguageEnum.Crab:
                    dialogue.text = ang;
                    break;
            }

            if(_icon == null)
            {
                icon.sprite = defaultIcon;
            }
            else
            {
                icon.sprite = _icon;
            }

            Show();
        }
    }
}