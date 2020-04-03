using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class Pastille : UIElement
    {
        public CameraSlider CameraSlider = default;

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

        public void SetHeight(float height)
        {
            coef = CameraSlider.tailleMap / height;
            Debug.Log(coef);

            rectTransform.anchoredPosition = new Vector3(0, CameraSlider.rectTransform.sizeDelta.y / coef, 0);
        }
    }
}