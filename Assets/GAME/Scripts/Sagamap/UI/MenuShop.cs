﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

namespace CrabMaga
{
    public class MenuShop : UIMenu
    {
        public PlayerData playerData = default;

        public GameObject[] headers = default;
        public GameObject[] pages = default;

        public Sprite Spr_UnselectedHeader, Spr_SelectedHeader;

        public void OpenPage(int index)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if(i == index)
                {
                    pages[i].SetActive(true);
                    headers[i].GetComponent<Image>().sprite = Spr_SelectedHeader;
                }
                else
                {
                    pages[i].SetActive(false);
                    headers[i].GetComponent<Image>().sprite = Spr_UnselectedHeader;
                }
            }
        }

        public void AchatCrab(int qte)
        {
            playerData.crabMoney += qte;
        }

        public void AchatShell(int qte)
        {
            playerData.shellMoney += qte;

        }

        public void AchatPearl(int qte)
        {
            playerData.pearlMoney += qte;

        }
    }
}