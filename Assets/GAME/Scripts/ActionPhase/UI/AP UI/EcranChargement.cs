using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.UI;

namespace CrabMaga
{
    public class EcranChargement : UIMenu
    {

        public override void Init()
        {
            base.Init();
            gameObject.SetActive(true);
            Hide();
        }

        public override void Show()
        {
            Time.timeScale = 1;

            base.Show();
        }
    }
}