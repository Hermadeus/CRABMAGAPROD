using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace QRTools.UI
{
    public class UIButton : UIElement, IUIInteractible
    {
        public Button button = default;

        public Image backGround = default;

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
            button.onClick.AddListener(OnClick.Invoke);
        }
    }
}