using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class SliderScrollView : MonoBehaviour
    {
        public ScrollRect scrollView;
        public Slider slider;

        private void Awake()
        {
            scrollView.onValueChanged.AddListener(OnValueChange);
            slider.minValue = scrollView.minHeight;
            slider.maxValue = scrollView.preferredHeight;
        }

        public void OnValueChange(Vector2 value)
        {

            //scrollView.OnValueChange

        }
    }
}