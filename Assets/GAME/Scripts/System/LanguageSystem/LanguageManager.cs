using UnityEngine;

using QRTools.Utilities.Observer;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Managers/LanguageSystem")]
    public class LanguageManager : ObserverScriptableObject, ISavable
    {        
        [SerializeField, EnumPaging] private LanguageEnum languageEnum = LanguageEnum.Francais;
        public LanguageEnum LanguageEnum
        {
            get => languageEnum;
            set
            {
                languageEnum = value;
                Debug.Log(value);
                UpdateObservable();
            }
        }

        public void ChangeLanguage(LanguageEnum _languageEnum)
        {
            LanguageEnum = _languageEnum;
            PersistableSO.Instance.Save();

            UpdateObservable();

        }

        public void Load()
        {
            LanguageEnum = (LanguageEnum)System.Enum.Parse(typeof(LanguageEnum), PlayerPrefs.GetString("LanguageEnum"));
        }

        public void Save()
        {
            PlayerPrefs.SetString("LanguageEnum", LanguageEnum.ToString());
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
        Anglais,
        Crab
    }
}