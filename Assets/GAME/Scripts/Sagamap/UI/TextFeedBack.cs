using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.UI;
using TMPro;
using DG.Tweening;

namespace CrabMaga
{
    public class TextFeedBack : MonoBehaviour
    {
        public RectTransform rt => GetComponent<RectTransform>();

        public CanvasGroup cg => GetComponent<CanvasGroup>();

        public TextMeshProUGUI text => GetComponent<TextMeshProUGUI>();

        Vector3 initialPos;

        private void Awake()
        {
            initialPos = rt.anchoredPosition;
        }

        public void Feedback(int money)
        {
            if(money > 0)
                text.text = "+" +  money.ToString();
            else
                text.text = "-" + money.ToString();

            rt.anchoredPosition = initialPos;
            cg.alpha = 1;

            rt.DOAnchorPosY(initialPos.y + 50f, .5f);
            cg.DOFade(0, .5f);
        }
    }
}