using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using UnityEngine.Events;

namespace CrabMaga
{
    public class ButtonSwitch : UIElement
    {
        public Switch[] switchs;



        public void TrySelect(string s)
        {
            for (int i = 0; i < switchs.Length; i++)
            {
                if (switchs[i].switchName == s)
                {
                    switchs[i].Select();
                }
                else
                {
                    switchs[i].Deselect();
                }
            }
        }
    }
}