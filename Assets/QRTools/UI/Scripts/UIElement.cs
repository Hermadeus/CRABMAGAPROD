using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using DG.Tweening;

namespace QRTools.UI
{
    public abstract class UIElement : MonoBehaviour, IUIElement
    {
        [HideInInspector] public RectTransform rectTransform;
        [HideInInspector] public CanvasGroup element;

        [BoxGroup("Properties"), Range(0,1)]
        public float timerAppear = .5f;
        [BoxGroup("Properties"), Range(0, 1)]
        public float timerDesappear = .5f;

        [FoldoutGroup("Events")]
        [SerializeField] private UnityEvent onShow = new UnityEvent();
        [FoldoutGroup("Events")]
        [SerializeField] private UnityEvent onHide = new UnityEvent();
        public UnityEvent OnHide { get => onHide; set => onHide = value; }
        public UnityEvent OnShow { get => onShow; set => onShow = value; }

        Tween show, hide;

        protected void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            TryGetComponent<CanvasGroup>(out element);
            Init();
        }

        public virtual void Hide()
        {
            HideElement(.5f);
        }

        public virtual void Show()
        {
            ShowElement(.5f);
        }

        public abstract void Init();

        public void HideElement(float timer)
        {
            show.Kill();
            hide = DOTween.To(() => element.alpha, x => element.alpha = x, 0, timerDesappear).SetEase(Ease.Linear);
            element.interactable = false;
        }

        public void ShowElement(float timer)
        {
            hide.Kill();
            show = DOTween.To(() => element.alpha, x => element.alpha = x, 1, timerAppear).SetEase(Ease.Linear);
            element.interactable = true;
        }
    }
}
