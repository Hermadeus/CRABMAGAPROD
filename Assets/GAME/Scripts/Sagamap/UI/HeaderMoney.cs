using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.UI;
using QRTools.Audio;

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

        public AudioEvent sonPiece;
        public AudioSource source;

        public override void Init()
        {
            base.Init();

            UpdateMoney();
        }

        public void UpdateMoney()
        {
            crabMoneyText.text = playerData.CrabMoney.ToString();

            if (playerData.CrabMoney == playerData.maxCrab)
                crabMoneyText.color = Color.red;
            else
                crabMoneyText.color = Color.white;

            shellMoneyText.text = playerData.shellMoney.ToString();
            pearlMoneyText.text = playerData.pearlMoney.ToString();
        }

        [Button]
        public void AddCrab(int x)
        {
            playerData.CrabMoney += x;
            crabMoneyText.text = playerData.CrabMoney.ToString();

            if (playerData.CrabMoney == playerData.maxCrab)
                crabMoneyText.color = Color.red;
            else
                crabMoneyText.color = Color.white;

            crab.Feedback(x);
            sonPiece.Play(source);
        }

        [Button]
        public void AddShell(int x)
        {
            playerData.shellMoney += x;
            shellMoneyText.text = playerData.shellMoney.ToString();
            sheel.Feedback(x);
            sonPiece.Play(source);
        }

        [Button]
        public void AddPearl(int x)
        {
            playerData.pearlMoney += x;
            pearlMoneyText.text = playerData.pearlMoney.ToString();
            pearl.Feedback(x);
            sonPiece.Play(source);
        }

        [Button]
        public void RemoveCrab(int x)
        {
            playerData.CrabMoney -= x;

            if (playerData.CrabMoney == playerData.maxCrab)
                crabMoneyText.color = Color.red;
            else
                crabMoneyText.color = Color.white;

            crabMoneyText.text = playerData.CrabMoney.ToString();
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

        [Button]
        public void Save()
        {
            PlayerPrefs.SetInt("crabMoney", playerData.crabMoney);
            PlayerPrefs.SetInt("pearlMoney", playerData.pearlMoney);
            PlayerPrefs.SetInt("shellMoney", playerData.shellMoney);
        }

        [Button]
        public void Load()
        {
            playerData.crabMoney = PlayerPrefs.GetInt("crabMoney");
            playerData.shellMoney = PlayerPrefs.GetInt("shellMoney");
            playerData.pearlMoney = PlayerPrefs.GetInt("pearlMoney");
        }
    }
}