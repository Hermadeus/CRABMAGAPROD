using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

using DG.Tweening;

namespace CrabMaga
{
    public class Pastille : UIElement
    {
        public Image icon = default;

        public CameraSlider CameraSlider = default;
        public Outline outline = default;

        [SerializeField] private bool isUsed = false;

        public Vector2 outlineSize = new Vector2(8f, 8f);

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

            outline.effectDistance = Vector2.zero;
        }

        public void SetHeight(float height)
        {
            coef = CameraSlider.tailleMap / height;

            rectTransform.anchoredPosition = new Vector3(0, CameraSlider.rectTransform.sizeDelta.y / coef, 0);

        }

        public void SetBackgroundPastille(Sprite spr)
        {
            icon.sprite = spr;

            StartCoroutine(SetEffectOutline());
        }

        IEnumerator SetEffectOutline()
        {
            for (int i = 0; i < 4; i++)
            {
                DOTween.To(() => outline.effectDistance, (x) => outline.effectDistance = x, outlineSize, .5f);
                yield return new WaitForSeconds(.5f);
                DOTween.To(() => outline.effectDistance, (x) => outline.effectDistance = x, Vector2.zero, .5f);
                yield return new WaitForSeconds(.5f);
            }

            yield break;
        }
    }
}