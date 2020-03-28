using System;

using QRTools.Utilities.Observer;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class TextLocalisation : Observable
    {
        [HideInInspector] public TextMeshProUGUI TextRef = default;
        public LanguageManager languageManager = default;
        public StringLanguage text = default;
        
        private void Awake()
        {
            Add(Observer);
            TextRef = GetComponent<TextMeshProUGUI>();
        }

        public override void Notify()
        {
            TextRef.text = text.GetCurrentText(languageManager.LanguageEnum);
        }

        [Button]
        void LinkReferences()
        {
            this.Observer = Resources.Load<LanguageManager>("Assets/GAME/Resources/System/LanguageSystem/Language Manager.asset") as IObserver;
            languageManager = Resources.Load<LanguageManager>("Assets/GAME/Resources/System/LanguageSystem/Language Manager.asset");
        }
    }
}