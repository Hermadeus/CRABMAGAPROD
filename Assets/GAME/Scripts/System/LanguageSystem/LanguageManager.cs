using UnityEngine;

using QRTools.Utilities.Observer;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Managers/LanguageSystem")]
    public class LanguageManager : ObserverScriptableObject
    {        
        [SerializeField, EnumPaging] private LanguageEnum languageEnum = LanguageEnum.Francais;
        public LanguageEnum LanguageEnum
        {
            get => languageEnum;
            set
            {
                languageEnum = value;
                UpdateObservable();
            }
        }

        public void ChangeLanguage(LanguageEnum _languageEnum)
        {
            LanguageEnum = _languageEnum;
        }

#if UNITY_EDITOR
        [Button]
        void ChangeLanguageInEditor(LanguageEnum _languageEnum)
        {
            languageEnum = _languageEnum;
            UpdateObservable();
        }
#endif
    }
    public enum LanguageEnum
    {
        Francais,
        Anglais
    }
}