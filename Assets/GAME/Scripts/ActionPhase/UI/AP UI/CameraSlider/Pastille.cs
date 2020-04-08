﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

namespace CrabMaga
{
    public class Pastille : UIElement
    {
        public CameraSlider CameraSlider = default;
        public Outline outline = default;

        private bool isUsed = false;

        public bool IsUsed
        {
            get => isUsed;
            set
            {
                isUsed = value;
                if (value)
                    gameObject.SetActive(true);
                else
                    gameObject.SetActive(false);
            }
        }

        float coef;

        public override void Init()
        {
            base.Init();

            rectTransform = GetComponent<RectTransform>();
            outline = GetComponent<Outline>();
        }

        public void SetHeight(float height)
        {
            coef = CameraSlider.tailleMap / height;

            rectTransform.anchoredPosition = new Vector3(0, CameraSlider.rectTransform.sizeDelta.y / coef, 0);
        }

        public void SetBackgroundPastille(Sprite spr)
        {
            background.sprite = spr;
        }
    }
}