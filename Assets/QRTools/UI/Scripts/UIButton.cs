using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using TMPro;

using Sirenix.OdinInspector;

namespace QRTools.UI
{
    public class UIButton : UIElement, IUIInteractible
    {
        public UIButtonParameters buttonParameters = default;
        public string parameterKey = "Default";

        public Button button = default;

        public TextMeshProUGUI title = default;

        [SerializeField] private Image icon = default;
        public Image Icon
        {
            get => icon;
            set
            {
                icon = value;
            }
        }

        [SerializeField] private bool isInteractible = true;
        public bool IsInteractible
        {
            get
            {
                return isInteractible;
            }
            set
            {
                isInteractible = value;
                if(value == true)
                {
                    button.interactable = true;
                }
                else
                {
                    button.interactable = false;
                }
            }
        }

        [SerializeField] private UnityEvent onClick = new UnityEvent();
        public UnityEvent OnClick { get => onClick; set => onClick = value; }

        public override void Hide()
        {
            IsInteractible = false;
        }

        public override void Show()
        {
            IsInteractible = true;
        }

        public override void Init()
        {
            TryGetComponent<Button>(out button);

            button.onClick.AddListener(OnClick.Invoke);

            if (buttonParameters != null)
            {
                UITheme theme = buttonParameters?.GetTheme(parameterKey);
                ColorBlock cb = theme.colorBlock;
                cb.selectedColor = cb.normalColor;
                button.colors = cb;
            }
        }

        [Button]
        void OnClickTest()
        {
            OnClick.Invoke();
        }
    }
}