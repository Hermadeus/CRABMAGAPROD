﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

using DG.Tweening;

namespace CrabMaga
{
    public class UnitWheelSlot : UIElement
    {
        public CrabUnitData entityDataRef = default;

        bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                if (value)
                {
                    if (background != null)
                        IsSelect();
                }
                else
                {
                    if (background != null)
                        IsDeselect();
                }
            }
        }

        [SerializeField] CanvasGroup canvasGroup = default;
        [SerializeField] Image crabThumbnailImage = default;

        Vector2 sizeDeltaBase = new Vector2();
        [SerializeField] Vector2 sizeDeltaOnSelect = new Vector2();

        public override void Init()
        {
            base.Init();

            sizeDeltaBase = rectTransform.sizeDelta;
        }

        public void InitSlot(CrabUnitData _entityData)
        {
            if(_entityData != null)
            {
                entityDataRef = _entityData;
                crabThumbnailImage.sprite = _entityData.wheelThumbnail;
            }
            else
                Hide();
        }

        public void OnSelect()
        {
            //Debug.Log("select " + gameObject.name);
        }

        public void IsSelect()
        {
            DOTween.To(
                () => canvasGroup.alpha,
                (x) => canvasGroup.alpha = x,
                1f,
                .2f);

            //DOTween.To(
            //    () => rectTransform.sizeDelta,
            //    (x) => rectTransform.sizeDelta = x,
            //    sizeDeltaOnSelect,
            //    .2f
            //    );
        }

        public void IsDeselect()
        {
            DOTween.To(
                () => canvasGroup.alpha,
                (x) => canvasGroup.alpha = x,
                .5f,
                .2f);

            //DOTween.To(
            //    () => rectTransform.sizeDelta,
            //    (x) => rectTransform.sizeDelta = x,
            //    sizeDeltaBase,
            //    .2f
            //    );
        }
    }
}