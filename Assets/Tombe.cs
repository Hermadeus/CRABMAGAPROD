using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    public class Tombe : MonoBehaviour
    {
        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 1f);
        }
    }
}