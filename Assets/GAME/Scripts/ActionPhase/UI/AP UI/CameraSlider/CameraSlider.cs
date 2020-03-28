using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;
using QRTools.Utilities.Observer;

namespace CrabMaga
{
    public class CameraSlider : UIElement, IObservable
    {
        public PlayerData playerData = default;

        [HideInInspector] public Slider slider = default;
        public Camera cameraObj = default;
        public Castle castle = default;

        public Vector2 clampedValue;

        float screenWidth;

        const float OFFSET = 3f;

        public override void Init()
        {
            base.Init();

            ShowElement();

            Add(playerData);

            screenWidth = Screen.width;
            ChangeHand();

            InitSlider();
        }

        Vector3 pos;

        void InitSlider()
        {
            slider = GetComponent<Slider>();

            clampedValue = new Vector2(OFFSET, castle.transform.position.z);
            slider.minValue = clampedValue.x;
            slider.maxValue = clampedValue.y;

            pos = new Vector3();
            pos.y = cameraObj.transform.position.y;

            slider.onValueChanged.AddListener(OnSliderMove);
        }

        public void OnSliderMove(float value)
        {
            pos.z = value - OFFSET;

            cameraObj.transform.position = pos;
        }

        public void Add(IObserver observer)
        {
            playerData.Observables.Add(this);
        }

        public void Notify()
        {
            ChangeHand();
        }

        public void Remove(IObserver observer)
        {
            throw new System.NotImplementedException();
        }

        void ChangeHand()
        {
            if (playerData.rightHand)
            {
                rectTransform.anchorMin = new Vector2(1, 0.5f);
                rectTransform.anchorMax = new Vector2(1, 0.5f);
                rectTransform.pivot = new Vector2(1, 0.5f);
            }
            else
            {
                rectTransform.anchorMin = new Vector2(0, 0.5f);
                rectTransform.anchorMax = new Vector2(0, 0.5f);
                rectTransform.pivot = new Vector2(0, 0.5f);
            }
        }
    }
}