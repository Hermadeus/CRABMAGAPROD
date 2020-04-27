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

        public override void OnClickButton()
        {
            base.OnClickButton();

            if(playerData.shellMoney >= menuArmy.currentTileSelected.entityData.currentPriceUpdate)
            {
                playerData.shellMoney -= menuArmy.currentTileSelected.entityData.currentPriceUpdate;
                headerMoney.UpdateMoney();
                menuArmy.currentTileSelected.entityData.UpgradeEntity();
                menuArmy.UpdateMenu(menuArmy.currentTileSelected);
            }
        }
    }
}