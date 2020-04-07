using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;
using QRTools.Utilities.Observer;

using Sirenix.OdinInspector;
using DG.Tweening;

namespace CrabMaga
{
    public class CameraSlider : UIElement, IObservable
    {
        public PlayerData playerData = default;

        [HideInInspector] public Slider slider = default;
        public Camera cameraObj = default;
        public Castle castle = default;

        [ReadOnly] public Vector2 clampedValue;

        float screenWidth;
        const float OFFSET = 3f;
        
        Vector3 pos;

        public Pastille[] pastilles = default;
        [HideInInspector] public float tailleMap;

        public override void Init()
        {
            base.Init();

            ShowElement();

            Add(playerData);

            screenWidth = Screen.width;
            ChangeHand();

            InitSlider();

            tailleMap = castle.transform.position.z;
        }


        void InitSlider()
        {
            slider = GetComponent<Slider>();

            clampedValue = new Vector2(OFFSET, castle.transform.position.z);
            slider.minValue = clampedValue.x;
            slider.maxValue = clampedValue.y;

            pos = new Vector3();
            pos.y = cameraObj.transform.position.y;

            slider.value = clampedValue.y;
            cameraObj.transform.position = new Vector3(cameraObj.transform.position.x, cameraObj.transform.position.y, clampedValue.y - OFFSET);

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

        public void SetSlider(float toValue, float timer = 2f)
        {
            DOTween.To(() => slider.value, (x) => slider.value = x, toValue, timer).SetEase(Ease.InOutSine);
        }

        public Pastille AddPastille(float height, Sprite spr)
        {
            Pastille pastille = null;

            for (int i = 0; i < pastilles.Length; i++)
            {
                if (pastilles[i].IsUsed == false)
                {
                    pastille = pastilles[i];
                    pastille.IsUsed = true;
                    break;
                }
            }

            if(spr != null && pastille != null)
                pastille.background.sprite = spr;

            float _coef = tailleMap / height;

            pastille.rectTransform.anchoredPosition = new Vector3(0, rectTransform.sizeDelta.y / _coef, 0);

            return pastille;
        }
    }
}