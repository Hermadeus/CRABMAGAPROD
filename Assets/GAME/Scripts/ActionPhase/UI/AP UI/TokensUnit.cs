using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace CrabMaga
{
    public class TokensUnit : MonoBehaviour
    {
        public static TokensUnit Instance;

        public List<TokenUnit> tokens = new List<TokenUnit>();

        public int startTokens = 5;

        public Image charge;
        public float timerCharge;

        public int CurrentTokenCount()
        {
            int x = 0;

            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].usable)
                    x++;
            }

            return x;
        }

        private void Awake()
        {
            Instance = this;

            for (int i = tokens.Count - 1; i > startTokens - 1; i--)
            {
                tokens[i].Close();
            }

            StartCoroutine(Charge());
        }

        IEnumerator Charge()
        {
            charge.fillAmount = 1;
            DOTween.To(
                () => charge.fillAmount,
                (x) => charge.fillAmount = x,
                0,
                timerCharge
                );
            yield return new WaitForSeconds(timerCharge);
            WinToken();
            StartCoroutine(Charge());
            yield break;
        }

        [Button]
        public void UseToken()
        {
            if (CurrentTokenCount() <= 0)
                return;

            tokens[CurrentTokenCount() - 1].Close();

            Debug.Log(CurrentTokenCount());
        }

        [Button]
        public void WinToken()
        {
            if (CurrentTokenCount() == tokens.Count)
                return;

            tokens[CurrentTokenCount()].Open(.5f);
        }
    }
}