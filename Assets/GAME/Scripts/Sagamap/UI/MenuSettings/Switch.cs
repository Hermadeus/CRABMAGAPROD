using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using QRTools.UI;

namespace CrabMaga {
    public class Switch : UIElement
    {
        public ButtonSwitch btnSwitch;
        public Image image;
        public Sprite btnOn, btnOff;
        public UnityEvent onclick;
        public string switchName = "";

        public void OnClick()
        {
            for (int i = 0; i < btnSwitch.switchs.Length; i++)
            {
                btnSwitch.switchs[i].Deselect();
            }
            Select();

            onclick.Invoke();

        }

        public void Deselect()
        {
            image.sprite = btnOff;
        }

        public void Select()
        {
            image.sprite = btnOn;
        }
    }
}