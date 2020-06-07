using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using QRTools.UI;

using DG.Tweening;

namespace CrabMaga
{
    public class ButtonLevel : UIElement
    {
        public bool isLock = false;

        public override void Show()
        {
            gameObject.SetActive(true);

            base.Show();

            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.interactable = !isLock;
            cg.blocksRaycasts = !isLock;
            if (isLock) cg.DOFade(.5f, .5f);
        }
    }
}