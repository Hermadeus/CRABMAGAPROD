using UnityEngine;

namespace CrabMaga
{
    [System.Serializable]
    public class StringLanguage
    {
        public string
            textFrancais = default,
            textAnglais = default;

        public string GetCurrentText(LanguageEnum languageEnum)
        {
            string text = "";

            switch (languageEnum)
            {
                case LanguageEnum.Francais:
                    text = textFrancais;
                    break;
                case LanguageEnum.Anglais:
                    text = textAnglais;
                    break;
            }

            return text;
        }
    }

    [System.Serializable]
    public class TextLanguage
    {
        [TextArea(3,5)]
        public string
            textFrancais = default,
            textAnglais = default;

        public string GetCurrentText(LanguageEnum languageEnum)
        {
            string text = "";

            switch (languageEnum)
            {
                case LanguageEnum.Francais:
                    text = textFrancais;
                    break;
                case LanguageEnum.Anglais:
                    text = textAnglais;
                    break;
            }

            return text;
        }
    }
}