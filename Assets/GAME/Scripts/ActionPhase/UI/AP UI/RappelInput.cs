using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class RappelInput : UIElement
    {
        public Animator anim = default;

        public bool asStop = false;

        public void Rapelle()
        {
            return;

            if (asStop) return;

            anim.SetTrigger("Rappel");
        }
    }
}