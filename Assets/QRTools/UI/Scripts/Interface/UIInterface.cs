using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace QRTools.UI
{
    public interface IUIElement
    {
        UnityEvent OnHide { get; set; }
        UnityEvent OnShow { get; set; }
        void Init();
        void Show();
        void Hide();
    }

    public interface IUIInteractible
    {
        UnityEvent OnClick { get; set; }
    }

    public interface IUIMenu
    {
        CanvasGroup Menu { get; set; }
    }
}