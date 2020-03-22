using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using QRTools.UI;
using QRTools.Utilities.Observer;

namespace CrabMaga
{
    public class TextButton : UIButton, IObservable
    {
        public LanguageManager languageManager = default;

        public TextMeshProUGUI text = default;
        public StringLanguage textLanguage;

        public override void Init()
        {
            base.Init();

            Add(languageManager);
            Notify();
        }

        public void Add(IObserver observer)
        {
            observer.Observables.Add(this);
        }

        public void Notify()
        {
            text.text = textLanguage.GetCurrentText(languageManager.LanguageEnum);
        }

        public void Remove(IObserver observer)
        {
            throw new System.NotImplementedException();
        }
    }
}
