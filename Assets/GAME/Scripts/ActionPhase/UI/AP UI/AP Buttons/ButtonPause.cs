using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class ButtonPause : UIButton
    {
        public AP_GameManager AP_GameManager = default;
        public MenuPause menuPause = default;

        public void Pause() => menuPause.Pause();
    }
}