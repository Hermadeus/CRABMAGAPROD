﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;

using TMPro;

using DG.Tweening;
using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class HeaderMoney : UIMenu
    {
        public PlayerData playerData = default;

        public TextMeshProUGUI
            crabMoneyText = default,
            shellMoneyText = default,
            pearlMoneyText = default;

        public TextFeedBack crab, sheel, pearl;

        public override void Init()
        {
            base.Init();

            UpdateMoney();
        }

        public void UpdateMoney()
        {
            crabMoneyText.text = playerData.CrabMoney.ToString() + "/" + playerData.maxCrab;
            shellMoneyText.text = playerData.shellMoney.ToString();
            pearlMoneyText.text = playerData.pearlMoney.ToString();
        }

        [Button]
        public void AddCrab(int x)
        {
            playerData.CrabMoney += x;
            crabMoneyText.text = playerData.CrabMoney.ToString() + "/" + playerData.maxCrab;
            crab.Feedback(x);
        }

        [Button]
        public void AddShell(int x)
        {
            playerData.shellMoney += x;
            shellMoneyText.text = playerData.shellMoney.ToString();
            sheel.Feedback(x);
        }

        [Button]
        public void AddPearl(int x)
        {
            playerData.pearlMoney += x;
            pearlMoneyText.text = playerData.pearlMoney.ToString();
            pearl.Feedback(x);
        }

        [Button]
        public void RemoveCrab(int x)
        {
            playerData.CrabMoney -= x;
            crabMoneyText.text = playerData.CrabMoney.ToString() + "/" + playerData.maxCrab;
            crab.Feedback(x);
        }

        [Button]
        public void RemoveShell(int x)
        {
            playerData.shellMoney -= x;
            shellMoneyText.text = playerData.shellMoney.ToString();
            sheel.Feedback(x);
        }

        [Button]
        public void RemovePearl(int x)
        {
            playerData.pearlMoney -= x;
            pearlMoneyText.text = playerData.pearlMoney.ToString();
            pearl.Feedback(x);
        }
    }
}