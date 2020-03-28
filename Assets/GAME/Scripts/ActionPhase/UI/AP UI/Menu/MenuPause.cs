using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class MenuPause : UIMenu
    {
        public ButtonPause buttonPause = default;
        public AP_GameManager AP_GameManager = default;

        public override void Show()
        {
            base.Show();
            AP_GameManager.InPause = true;
            anim.timeScale = 1;
        }

        public override void Hide()
        {
            base.Hide();
            AP_GameManager.InPause = false;
            anim.timeScale = 1;
        }

        public void Pause()
        {
            if (AP_GameManager.InPause)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }
}