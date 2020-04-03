using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using TMPro;

using Sirenix.OdinInspector;

using DG.Tweening;

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

        delegate void AnimEvent();
        AnimEvent animEvent;

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

            button.onClick.AddListener(OnClickButton);

            if (buttonParameters != null)
            {
                UITheme theme = buttonParameters?.GetTheme(parameterKey);
                ColorBlock cb = theme.colorBlock;
                cb.selectedColor = cb.normalColor;
                button.colors = cb;
            }

            InitAnim();
        }

        public virtual void OnClickButton()
        {
            OnClick.Invoke();

            animEvent.Invoke();
        }

        [Button]
        void OnClickTest()
        {
            OnClick.Invoke();
        }

        void InitAnim()
        {
            animEvent += AnimDezoom;
        }

        void AnimDezoom()
        {
            rectTransform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), .2f).SetEase(Ease.InOutSine).OnComplete(SetScaleToOne);
        }

        void SetScaleToOne()
        {
            rectTransform.DOScale(new Vector3(1, 1, 1), .2f).SetEase(Ease.InOutSine);
        }
    }
}