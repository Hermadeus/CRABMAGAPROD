using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using QRTools.Variables;
using DG.Tweening;

namespace CrabMaga
{
    public class JaugeDeConquete : UIElement
    {
        public Slider slider;

        public int maxSliderValue;
        public Vector2IntVariable currentXP; //x => current & y => previous

        public PlayerData playerData;
        public HeaderMoney headerMoney;

        public Pallier[] palliers;

        public override void Init()
        {
            base.Init();

            slider.maxValue = maxSliderValue;
            slider.value = currentXP.Value.y;

            UpdateXP();
        }

        public void UpdateXP()
        {
            StartCoroutine(Up());
        }

        IEnumerator Up()
        {
            int diff = currentXP.Value.x - currentXP.Value.y;

            if(slider.value + diff >= slider.maxValue)
            {
                DOTween.To(
                () => slider.value,
                (x) => slider.value = x,
                slider.maxValue,
                2f
                ).OnComplete(delegate { Rest(diff % maxSliderValue); }); ;
            }
            else
            {
                DOTween.To(
                () => slider.value,
                (x) => slider.value = x,
                currentXP.Value.x,
                2f
                );
            }

            yield return new WaitForSeconds(3f);

            for (int i = 0; i < currentXP.Value.x; i++)
            {
                if (!palliers[i].isWin)
                {
                    AddRecompense(palliers[i]);
                    palliers[i].isWin = true;
                }
            }

            currentXP.SetValueY(currentXP.Value.x);


            yield break;
        }

        void Rest(int v)
        {
            int diff = currentXP.Value.x - currentXP.Value.y;

            slider.value = 0;

            DOTween.To(
                () => slider.value,
                (x) => slider.value = x,
                v,
                2f);
        }

        public void AddRecompense(Pallier p)
        {
            playerData.crabMoney += p.recompenseCrab;
            playerData.shellMoney += p.recompenseShell;
            playerData.pearlMoney += p.recompensePearl;
            headerMoney.UpdateMoney();

            p.isWin = true;
        }
    }

    [System.Serializable]
    public class Pallier
    {
        public int recompenseCrab;
        public int recompenseShell;
        public int recompensePearl;

        public bool isWin = false;
    }
}