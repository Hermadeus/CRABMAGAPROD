using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;
using QRTools.Variables;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class JaugeDeConquete : UIElement
    {
        public Slider slider;
        public Vector2IntVariable currentXP; //x => current & y => previous
        public PlayerData playerData;
        public HeaderMoney headerMoney;
        public TextAsset csvFile;

        public int currentNiveau;
        public NiveauDeJauge[] niveauDeJauges;

        public float animSpeed;

        public override void Init()
        {
            base.Init();

            Debug.Log("jauge conquete init");

            slider.maxValue = niveauDeJauges[currentNiveau].starMax;
            slider.value = currentXP.GetValueY();

            Invoke("AddXP", 1f);
        }

        public void AddXP()
        {
            int XPToAdd = currentXP.Value.x - currentXP.Value.y;

            if(slider.value + XPToAdd < slider.maxValue)
                DOTween.To(() => slider.value, (x) => slider.value = x, slider.value + XPToAdd, animSpeed).SetEase(Ease.InOutSine);
            else if (slider.value + XPToAdd >= slider.maxValue)
            {
                StartCoroutine(AddXPAndReset(currentXP.GetValueX() - currentXP.GetValueY()));
            }
        }

        IEnumerator AddXPAndReset(int XPToAdd)
        {
            int diff = ((int)slider.value + XPToAdd) - (int)slider.maxValue;

            if (slider.value + diff > slider.maxValue)
            {
                int diffdiff = (int)slider.maxValue - (int)slider.value;

                currentXP.SetValueY(currentXP.GetValueY() + diffdiff);
                StartCoroutine(AddXPAndReset(currentXP.GetValueX() - currentXP.GetValueY()));
                currentXP.SetValueY(currentXP.GetValueX());
                yield break;
            }

            DOTween.To(() => slider.value, (x) => slider.value = x, slider.maxValue, animSpeed).SetEase(Ease.InSine);

            yield return new WaitForSeconds(animSpeed + .5f);
            currentNiveau++;
            OnLevelUp();            

            slider.value = 0;
            slider.maxValue = niveauDeJauges[currentNiveau].starMax;

            DOTween.To(() => slider.value, (x) => slider.value = x, diff, animSpeed).SetEase(Ease.InSine);

            currentXP.SetValueY(currentXP.GetValueX());

            yield break;
        }

        public void OnLevelUp()
        {
           // playerData.shellMoney += niveauDeJauges[currentNiveau].shellReward;
            headerMoney.AddShell(niveauDeJauges[currentNiveau].shellReward);
        }

        [Button]
        public void InitNiveauDeJauge()
        {
            int _size = 20;

            string[,] s = ParseCSV();

            for (int i = 1; i < _size + 1; i++)
            {
                string[] c2 = s[1, i].Split(';');
                int shell = int.Parse(c2[0]);
                niveauDeJauges[i].shellReward = shell;

                string[] c3 = s[2, i].Split(';');
                int starMax = int.Parse(c3[0]);
                niveauDeJauges[i].starMax = starMax;
            }
        }

        public string[,] ParseCSV()
        {
            //split the data on split line character
            string[] lines = csvFile.text.Split("\n"[0]);

            // find the max number of columns
            int totalColumns = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(';');
                totalColumns = Mathf.Max(totalColumns, row.Length);
            }

            // creates new 2D string grid to output to
            string[,] outputGrid = new string[totalColumns + 1, lines.Length + 1];
            for (int y = 0; y < lines.Length; y++)
            {
                string[] row = lines[y].Split(';');
                for (int x = 0; x < row.Length; x++)
                {
                    outputGrid[x, y] = row[x];
                }
            }

            //Debug.Log("L = " + lines.Length);
            //Debug.Log("C = " + totalColumns);

            return outputGrid;
        }

        //    public void UpdateXP()
        //    {
        //        StartCoroutine(Up());
        //    }

        //    IEnumerator Up()
        //    {
        //        int diff = currentXP.Value.x - currentXP.Value.y;

        //        Debug.Log(diff);

        //        if (slider.value + diff > slider.maxValue)
        //        {
        //            DOTween.To(
        //            () => slider.value,
        //            (x) => slider.value = x,
        //            slider.maxValue,
        //            2f
        //            ).OnComplete(delegate { Rest(diff % maxSliderValue); }); ;
        //        }
        //        else
        //        {
        //            DOTween.To(
        //            () => slider.value,
        //            (x) => slider.value = x,
        //            currentXP.Value.x,
        //            2f
        //            );
        //        }

        //        yield return new WaitForSeconds(3f);

        //        for (int i = 0; i < currentXP.Value.x; i++)
        //        {
        //            if (!palliers[i].isWin)
        //            {
        //                AddRecompense(palliers[i]);
        //                palliers[i].isWin = true;
        //            }
        //        }

        //        currentXP.SetValueY(currentXP.Value.x);

        //        yield break;
        //    }

        //    void Rest(int v)
        //    {
        //        int diff = currentXP.Value.x - currentXP.Value.y;

        //        slider.value = 0;

        //        DOTween.To(
        //            () => slider.value,
        //            (x) => slider.value = x,
        //            v,
        //            2f);
        //    }

        //    public void AddRecompense(Pallier p)
        //    {
        //        playerData.crabMoney += p.recompenseCrab;
        //        playerData.shellMoney += p.recompenseShell;
        //        playerData.pearlMoney += p.recompensePearl;
        //        headerMoney.UpdateMoney();

        //        p.isWin = true;
        //    }

        //    [Button]
        //    public void AddXP()
        //    {
        //        currentXP.SetValueX(currentXP.GetValueX() + 1);
        //        Init();
        //    }

        //    [Button]
        //    public void ResetXP()
        //    {
        //        currentXP.SetValueX(0);
        //        currentXP.SetValueY(0);
        //        Init();
        //    }
        //}

        [System.Serializable]
        public class NiveauDeJauge
        {
            public int starMax;
            public int shellReward;
        }
    }
}