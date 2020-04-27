using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;

namespace CrabMaga
{
    public class MenuChoixUnit : UIMenu
    {

        public CanvasGroup unitTab, leaderTab;
        public Image btnTabUnit, btnTabLeader;
        public Sprite selectedSprite, unselectedSprite;

        public void LeaderSelection()
        {
            btnTabUnit.GetComponent<Image>().sprite = unselectedSprite;
            unitTab.alpha = 0;
            unitTab.interactable = false;
            unitTab.blocksRaycasts = false;

            btnTabLeader.GetComponent<Image>().sprite = selectedSprite;
            leaderTab.alpha = 1;
            leaderTab.interactable = true;
            leaderTab.blocksRaycasts = true;
        }

        public void UnitsSelection()
        {
            btnTabLeader.GetComponent<Image>().sprite = unselectedSprite;
            leaderTab.alpha = 0;
            leaderTab.interactable = false;
            leaderTab.blocksRaycasts = false;

            btnTabUnit.GetComponent<Image>().sprite = selectedSprite;
            unitTab.alpha = 1;
            unitTab.interactable = true;
            unitTab.blocksRaycasts = true;
        }
    }
}