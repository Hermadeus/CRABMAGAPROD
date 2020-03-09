using System;
using QRTools.Utilities.Observer;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    }
}