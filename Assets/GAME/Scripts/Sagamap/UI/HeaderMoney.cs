using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;

using TMPro;

namespace CrabMaga
{
    public class HeaderMoney : UIMenu
    {
        public PlayerData playerData = default;

        public TextMeshProUGUI
            crabMoneyText = default,
            shellMoneyText = default,
            pearlMoneyText = default;

        public override void Init()
        {
            base.Init();

            UpdateMoney();
        }

        public void UpdateMoney()
        {
            crabMoneyText.text = playerData.crabMoney.ToString();
            shellMoneyText.text = playerData.shellMoney.ToString();
            pearlMoneyText.text = playerData.pearlMoney.ToString();
        }
    }
}