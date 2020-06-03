using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using QRTools.UI;

using DG.Tweening;

namespace CrabMaga
{
    public class UnitWheelSlot : UIElement
    {
        public CrabUnitData entityDataRef = default;

        public UnityEvent onTuto;

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
        public Image crabThumbnailImage = default;

        Vector2 sizeDeltaBase = new Vector2();
        [SerializeField] Vector2 sizeDeltaOnSelect = new Vector2();

        public UnitWheelInfo info;

        public override void Init()
        {
            base.Init();

            sizeDeltaBase = rectTransform.sizeDelta;
        }

        public void InitSlot(CrabUnitData _entityData)
        {
            if(_entityData == null)
            {
                Hide();
                Debug.Log("hide");
                HideElement();
                return;
            }

            if(_entityData != null)
            {
                entityDataRef = _entityData;
                crabThumbnailImage.sprite = _entityData.wheelThumbnail;
            }
            else
                Hide();

            if (_entityData != null)
            {
                if (info != null)
                    info.cost.text = entityDataRef.costUnit.ToString();
            }
            else
                canvasGroup.alpha = 0;
        }

        public void OnSelect()
        {
            //Debug.Log("select " + gameObject.name);

            onTuto?.Invoke();

            if (entityDataRef == null)
                Hide();
        }

        public void IsSelect()
        {
            DOTween.To(
                () => canvasGroup.alpha,
                (x) => canvasGroup.alpha = x,
                1f,
                .2f);

            info?.Show();

            //DOTween.To(
            //    () => rectTransform.sizeDelta,
            //    (x) => rectTransform.sizeDelta = x,
            //    sizeDeltaOnSelect,
            //    .2f
            //    );
        }

        public void IsDeselect()
        {
            if (entityDataRef == null)
                return;

            DOTween.To(
                () => canvasGroup.alpha,
                (x) => canvasGroup.alpha = x,
                .5f,
                .2f);

            info?.Hide();

            //DOTween.To(
            //    () => rectTransform.sizeDelta,
            //    (x) => rectTransform.sizeDelta = x,
            //    sizeDeltaBase,
            //    .2f
            //    );
        }
    }
}