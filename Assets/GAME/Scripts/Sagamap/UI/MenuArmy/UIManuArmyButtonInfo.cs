using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class UIManuArmyButtonInfo : MonoBehaviour
    {
        public bool onStat = true;

        public CanvasGroup stats, desc, btnUp;

        public void OnClick()
        {
            if (onStat)
            {
                onStat = false;
                stats.alpha = 0;
                desc.alpha = 1;

                btnUp.alpha = 0;
                btnUp.interactable = false;
                btnUp.blocksRaycasts = false;
            }
            else
            {
                onStat = true;
                stats.alpha = 1;
                desc.alpha = 0;

                btnUp.alpha = 1;
                btnUp.interactable = true;
                btnUp.blocksRaycasts = true;
            }
        }
    }
}