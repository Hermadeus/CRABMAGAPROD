using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.UI;
using DG.Tweening;

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
            if (islock)
            {
                if (playerData.shellMoney - 50 > 0)
                {
                    headerMoney.RemoveShell(menuArmy.currentTileSelected.entityData.currentPriceUpdate);
                    menuArmy.currentTileSelected.entityData.isLock = false;
                    menuArmy.currentTileSelected.Unlock();
                    menuArmy.Init();
                    Islock = false;
                }
                else
                {
                    rectTransform.DOShakeAnchorPos(.5f);
                }
            }

            base.OnClickButton();

            if(playerData.shellMoney >= menuArmy.currentTileSelected.entityData.currentPriceUpdate)
            {
                headerMoney.RemoveShell(menuArmy.currentTileSelected.entityData.currentPriceUpdate);
                menuArmy.currentTileSelected.entityData.UpgradeEntity();
                menuArmy.UpdateMenu(menuArmy.currentTileSelected);
                menuArmy.Save();
            }
        }
    }
}