using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.UI;

namespace CrabMaga
{
    public class AmeliorationButton : UIButton
    {
        public UIMenuArmy menuArmy;
        public PlayerData playerData = default;
        public HeaderMoney headerMoney = default;
        public GameObject cadenas;

        [SerializeField] private bool islock = false;

        public bool Islock
        {
            get => islock;
            set
            {
                islock = value;
                if (value)
                {
                    cadenas.SetActive(value);
                }
                else
                {
                    cadenas.SetActive(value);
                }
            }
        }

        public override void OnClickButton()
        {
            if (islock) return;

            base.OnClickButton();

            if(playerData.shellMoney >= menuArmy.currentTileSelected.entityData.currentPriceUpdate)
            {
                playerData.shellMoney -= menuArmy.currentTileSelected.entityData.currentPriceUpdate;
                headerMoney.UpdateMoney();
                menuArmy.currentTileSelected.entityData.UpgradeEntity();
                menuArmy.UpdateMenu(menuArmy.currentTileSelected);
                menuArmy.Save();
            }
        }
    }
}