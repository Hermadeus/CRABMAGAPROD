using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class MenuSettings : UIMenu
    {
        public enum MenuPos { SAGAMAP, ACTION_PHASE}
        public MenuPos menuPos = MenuPos.SAGAMAP;

        public LanguageManager languageManager;
        public PlayerData playerData;

        public SoundManager SoundManager;

        public override void Init()
        {
            base.Init();
            StartCoroutine(T());
        }

        IEnumerator T()
        {
            yield return new WaitForSeconds(.1f);
            Close();
            yield break;
        }

        public void EnglishMode()
        {
            languageManager.ChangeLanguage(LanguageEnum.Anglais);
        }

        public void FrenchMode()
        {
            languageManager.ChangeLanguage(LanguageEnum.Francais);
        }

        public void CrabMode()
        {
            languageManager.ChangeLanguage(LanguageEnum.Crab);
        }

        public void RightMode()
        {
            playerData.RightHand = true;

        }

        public void LeftMode()
        {
            playerData.RightHand = false;
        }

        public void Credit()
        {

        }

        public void Open()
        {
            Menu.alpha = 1;
            Menu.interactable = true;
            Menu.blocksRaycasts = true;
            PersistableSO.Instance.Save();

        }

        public void Close()
        {
            Menu.alpha = 0;

            Menu.interactable = false;
            Menu.blocksRaycasts = false;
            PersistableSO.Instance.Save();

        }
    }
}
