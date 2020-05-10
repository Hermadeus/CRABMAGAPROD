using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace CrabMaga
{
    public class TokenUnit : MonoBehaviour
    {
        public Image token;
        public bool usable = true;
        public Tween tween;

        public void Close()
        {
            token.color = new Color(
                token.color.r,
                token.color.g,
                token.color.b,
                0
                );

            usable = false;
        }

        public void Close(float t)
        {
            tween?.Kill();
            tween = token.DOColor(
                new Color(
                token.color.r,
                token.color.g,
                token.color.b,
                0),
                t
                );

            usable = false;
        }

        public void Open(float t)
        {
            tween?.Kill();
            tween = token.DOColor(
                new Color(
                token.color.r,
                token.color.g,
                token.color.b,
                1),
                t
                );

            usable = true;
        }
    }
}