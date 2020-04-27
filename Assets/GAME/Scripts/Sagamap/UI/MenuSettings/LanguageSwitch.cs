using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.UI;

namespace CrabMaga
{
    public class LanguageSwitch : ButtonSwitch
    {
        public LanguageManager languageManager;

        public override void Init()
        {
            base.Init();

            switch (languageManager.LanguageEnum)
            {
                case LanguageEnum.Francais:
                    TrySelect("francais");
                    break;
                case LanguageEnum.Anglais:
                    TrySelect("anglais");
                    break;
                case LanguageEnum.Crab:
                    TrySelect("crab");
                    break;
            }
        }
    }
}