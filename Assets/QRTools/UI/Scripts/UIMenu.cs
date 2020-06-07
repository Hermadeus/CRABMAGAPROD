﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using DG.Tweening;

using CrabMaga;

namespace QRTools.UI
{
    public class UIMenu : UIElement, IUIMenu
    {
        [FoldoutGroup("References")]
        [SerializeField] private CanvasGroup menu = default;
        public CanvasGroup Menu { get => menu; set { menu = value; } }

        [FoldoutGroup("ApparitionMode")]
        public ApparitionMode apparitionMode = ApparitionMode.FADE;
        [FoldoutGroup("ApparitionMode")]
        public float fadeTimer = .5f;
        [FoldoutGroup("ApparitionMode")]
        public Ease ease = Ease.Linear;

        delegate void AnimationFct();
        AnimationFct animShow;
        AnimationFct animHide;
        [ShowIf("apparitionMode", ApparitionMode.ANIMATION), FoldoutGroup("ApparitionMode")]
        public AnimationMode animationMode = AnimationMode.DOWN;
        [ShowIf("apparitionMode", ApparitionMode.ANIMATION), FoldoutGroup("ApparitionMode")]
        public float animSpeed = 1f;

        protected Tween anim;

        public Canvas c;

        public bool startHide;

        public override void Init()
        {
            base.Init();

            TryGetComponent<CanvasGroup>(out menu);
            
            if(apparitionMode == ApparitionMode.ANIMATION)
                InitAnim();

            c = FindObjectOfType<CanvasPrinc>()?.canvas;

            if (startHide)
                gameObject.SetActive(false);
        }

        [ButtonGroup("Debug")]
        public override void Show()
        {
            gameObject.SetActive(true);

            menu.blocksRaycasts = true;
            menu.interactable = true;

            switch (apparitionMode)
            {
                case ApparitionMode.FADE:
                    float to = 1;
                    anim = DOTween.To(() => menu.alpha, x => menu.alpha = x, to, fadeTimer).SetEase(ease).OnComplete(OnHide.Invoke);
                    break;
                case ApparitionMode.ANIMATION:
                    animShow.Invoke();
                    break;
            }
        }

        [ButtonGroup("Debug")]
        public override void Hide()
        {
            menu.blocksRaycasts = false;
            menu.interactable = false;

            switch (apparitionMode)
            {
                case ApparitionMode.FADE:
                    float to = 0;
                    anim = DOTween.To(() => menu.alpha, x => menu.alpha = x, to, fadeTimer).SetEase(ease).OnComplete(OnHide.Invoke);
                    break;
                case ApparitionMode.ANIMATION:
                    animHide.Invoke();
                    break;
            }

            Invoke("SetOff", animSpeed);
        }

        void SetOff() => gameObject.SetActive(false);

        void InitAnim()
        {
            switch (animationMode)
            {
                case AnimationMode.RIGHT:
                    animShow += Appear;
                    animHide += RightDesappear;
                    break;
                case AnimationMode.LEFT:
                    animShow += Appear;
                    animHide += LeftDesappear;
                    break;
                case AnimationMode.UP:
                    animShow += Appear;
                    animHide += UpDesappear;
                    break;
                case AnimationMode.DOWN:
                    animShow += Appear;
                    animHide += DownDesappear;
                    break;
                case AnimationMode.PINCH:
                    animShow += PinchAppear;
                    animHide += PinchDesappear;
                    break;
                case AnimationMode.STRETCH:
                    animShow += StrechAppear;
                    animHide += StretchDesappear;
                    break;
            }
        }

        void Appear()
        {
            SetAlphaToOne();
            rectTransform.DOAnchorPos(new Vector2(0, 0), animSpeed).SetEase(ease);
            OnShow.Invoke();
        }

        void RightDesappear()
        {
            rectTransform.DOAnchorPos(new Vector2(3000, 0), animSpeed).SetEase(ease).OnComplete(OnHide.Invoke);
        }

        void LeftDesappear()
        {
            rectTransform.DOAnchorPos(new Vector2(-3000, 0), animSpeed).SetEase(ease).OnComplete(OnHide.Invoke);
        }

        void DownDesappear()
        {
            rectTransform.DOAnchorPos(new Vector2(0, -3000), animSpeed).SetEase(ease).OnComplete(OnHide.Invoke);
        }

        void UpDesappear()
        {
            rectTransform.DOAnchorPos(new Vector2(0, 3000), animSpeed).SetEase(ease).OnComplete(OnHide.Invoke);
        }

        void PinchAppear()
        {
            rectTransform.DOScale(Vector3.one, animSpeed).SetEase(ease).OnComplete(OnShow.Invoke);
            SetAlphaToOne();
        }

        void PinchDesappear()
        {
            rectTransform.DOScale(Vector3.zero, animSpeed).SetEase(ease).OnComplete(OnHide.Invoke);
            SetAlphaToZero(fadeTimer);
        }

        void StrechAppear()
        {
            rectTransform.sizeDelta = Vector3.one * 2;
            rectTransform.DOScale(Vector3.one, animSpeed).SetEase(ease).OnComplete(OnShow.Invoke);
            SetAlphaToOne(fadeTimer);
        }

        void StretchDesappear()
        {
            rectTransform.DOScale(Vector3.zero, animSpeed).SetEase(ease).OnComplete(OnHide.Invoke);
            SetAlphaToZero(fadeTimer);
        }

        public void SetAlphaToZero()
        {
            Menu.alpha = 0;
        }

        public void SetAlphaToZero(float timer)
        {
            anim = DOTween.To(() => menu.alpha, x => menu.alpha = x, 0, timer).SetEase(Ease.Linear);
        }

        public void SetAlphaToOne()
        {
            Menu.alpha = 1;
        }

        public void SetAlphaToOne(float timer)
        {
            anim = DOTween.To(() => menu.alpha, x => menu.alpha = x, 1, timer).SetEase(Ease.Linear);
        }
    }

    public enum ApparitionMode
    {
        FADE,
        ANIMATION,
    }

    public enum AnimationMode
    {
        RIGHT,
        LEFT,
        UP,
        DOWN,
        PINCH,
        STRETCH,
    }
}