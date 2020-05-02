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

        Coroutine a;

        public void AnimationOnDeath(EntityData data)
        {
            Animate(data.pastilleSprite, .08f, data.pastilleDeath);
        }

        public void AnimationOnDetection(EntityData data)
        {
            Animate(data.pastilleSprite, .08f, data.pastilleDetection);

        }

        public void AnimationOnReachCastle(EntityData data)
        {
            Animate(data.pastilleSprite, .08f, data.pastilleOnReachCastle);

        }

        public void AnimateOnInstantiation(EntityData data)
        {
            Animate(data.pastilleSprite, .08f, data.pastilleOnInstantiation);

        }

        public void AnimateOnAttack(EntityData data)
        {
            Animate(data.pastilleSprite, .08f, data.pastilleAttack);

        }

        public void OnLosePV(EntityData data) //Pour le général
        {
            Animate(data.pastilleSprite, .08f, data.pastilleOnLosePV);

        }

        public void Animate(Sprite defaultSpr, float speed, params Sprite[] spr)
        {
            if(a != null)
            {
                StopCoroutine(a);
                icon.sprite = defaultSpr;
            }

            StartCoroutine(A(speed, spr));
        }

        IEnumerator A(float speed, params Sprite[] spr)
        {
            for (int i = 0; i < spr.Length; i++)
            {
                icon.sprite = spr[i];
                yield return new WaitForSeconds(speed);
            }

            a = null;
            yield break;
        }
    }
}