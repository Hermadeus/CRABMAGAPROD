using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class VenteTile : MonoBehaviour
    {
        public int cost;
        public int ressourceToAdd;

        public MenuShop menushop;

        public Button btn;

        public AchatType achatType;

        private void Awake()
        {
            btn.onClick.AddListener(Achat);
        }

        public void Achat()
        {
            switch (achatType)
            {
                case AchatType.CRAB:

                    if(menushop.playerData.pearlMoney - cost < 0)
                    {
                        menushop.FeedBackAchatNonIntegre(false);
                        break;
                    }

                    menushop.AchatCrab(ressourceToAdd);
                    menushop.headerMoney.AddPearl(-cost);
                    break;
                case AchatType.PEARL:
                    menushop.AchatPearl(ressourceToAdd);

                    break;
                case AchatType.SHELL:
                    if (menushop.playerData.pearlMoney - cost < 0)
                    {
                        menushop.FeedBackAchatNonIntegre(false);
                        break;
                    }
                    menushop.AchatShell(ressourceToAdd);
                    menushop.headerMoney.AddPearl(-cost);
                    break;
            }
        }

    }

    public enum AchatType { CRAB, PEARL, SHELL}
}