using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using QRTools.UI;

namespace CrabMaga
{
    public class CrabCount : UIElement
    {
        public PlayerData playerData = default;

        public TextMeshProUGUI crabQuantiteText = default;

        public override void Init()
        {
            base.Init();

            UpdateText();
        }

        public void UpdateText()
        {
            crabQuantiteText.text = playerData.crabMoney.ToString();

            if (playerData.crabMoney == 0)
                animator.SetBool("warn", true);
            else
                animator.SetBool("warn", false);

        }

        public Animator animator;
    }
}