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
            gameObject.SetActive(true);

            base.Init();
            gameObject.SetActive(true);
            Hide();
        }
    }
}