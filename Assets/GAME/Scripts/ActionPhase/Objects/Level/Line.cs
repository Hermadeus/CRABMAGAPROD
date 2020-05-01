using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CrabMaga
{
    public class Line : MonoBehaviour
    {
        SpriteRenderer sr;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public void Select()
        {
            sr.DOColor(
                new Color(sr.color.r, sr.color.g, sr.color.b, .5f),
                .2f
                );
        }

        public void Unselect()
        {
            sr.DOColor(
                new Color(sr.color.r, sr.color.g, sr.color.b, 0),
                .2f
                );
        }
    }
}