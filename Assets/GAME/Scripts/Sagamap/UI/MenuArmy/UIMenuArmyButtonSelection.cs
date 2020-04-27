using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;

namespace CrabMaga
{
    public class UIMenuArmyButtonSelection : MonoBehaviour
    {
        public Image back;
        public Sprite selectedSprite, unselectedSprite;

        public void Select()
        {
            back.sprite = selectedSprite;
        }

        public void UnSelect()
        {
            back.sprite = unselectedSprite;
        }
    }
}