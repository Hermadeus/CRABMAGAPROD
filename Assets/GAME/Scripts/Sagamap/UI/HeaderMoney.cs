using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;

using TMPro;

using DG.Tweening;

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

        public void AddCrab(int x)
        {
            playerData.crabMoney += x;
            crabMoneyText.text = playerData.crabMoney.ToString();
        }

        public void AddShell(int x)
        {
            playerData.shellMoney += x;
            shellMoneyText.text = playerData.shellMoney.ToString();
        }

        public void AddPearl(int x)
        {
            playerData.pearlMoney += x;
            pearlMoneyText.text = playerData.pearlMoney.ToString();
        }
    }
}