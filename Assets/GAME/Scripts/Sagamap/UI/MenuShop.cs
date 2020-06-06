using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

using TMPro;
using DG.Tweening;

namespace CrabMaga
{
    public class MenuShop : UIMenu
    {
        public PlayerData playerData = default;

        public GameObject[] headers = default;
        public GameObject[] pages = default;

        public Sprite[] Spr_UnselectedHeader, Spr_SelectedHeader;

        public VenteTile[] crabsAchats, shellAchats, pearlAchat;

        public HeaderMoney headerMoney;

        public LanguageManager languageManager;
        public TextMeshProUGUI textPopUp;
        public CanvasGroup AchatIndisponibleWIP;

        public override void Init()
        {
            base.Init();

            headerMoney = FindObjectOfType<HeaderMoney>();

            for (int i = 0; i < crabsAchats.Length; i++)
            {
                crabsAchats[i].menushop = this;
            }
            for (int i = 0; i < shellAchats.Length; i++)
            {
                shellAchats[i].menushop = this;
            }
            for (int i = 0; i < pearlAchat.Length; i++)
            {
                pearlAchat[i].menushop = this;
            }
        }

        public void OpenPage(int index)
        {
            for (int i = 0; i < pages.Length; i++)
            {
                if(i == index)
                {
                    pages[i].SetActive(true);
                    headers[i].GetComponent<Image>().sprite = Spr_SelectedHeader[i];
                }
                else
                {
                    pages[i].SetActive(false);
                    headers[i].GetComponent<Image>().sprite = Spr_UnselectedHeader[i];
                }
            }
        }

        public void AchatCrab(int qte)
        {
            headerMoney.AddCrab(qte);
        }

        public void AchatShell(int qte)
        {
            headerMoney.AddShell(qte);
        }

        public void AchatPearl(int qte)
        {
            headerMoney.AddPearl(qte);
            FeedBackAchatNonIntegre(true);
        }

        Coroutine c;
        public void FeedBackAchatNonIntegre(bool isPopup = false)
        {
            AchatIndisponibleWIP.alpha = 0;

            if (c != null) StopCoroutine(c);
            c = StartCoroutine(FeedbackAchatIndispo(isPopup));
        }

        public IEnumerator FeedbackAchatIndispo(bool isPopup = false)
        {
            if (isPopup)
            {
                switch (languageManager.LanguageEnum)
                {
                    case LanguageEnum.Francais:
                        textPopUp.text = "achat indisponible";

                        break;
                    case LanguageEnum.Anglais:
                        textPopUp.text = "";

                        break;
                    case LanguageEnum.Crab:
                        textPopUp.text = "";

                        break;
                }
            }
            else
            {
                switch (languageManager.LanguageEnum)
                {
                    case LanguageEnum.Francais:
                        textPopUp.text = "pas assez de tune";

                        break;
                    case LanguageEnum.Anglais:
                        textPopUp.text = "";

                        break;
                    case LanguageEnum.Crab:
                        textPopUp.text = "";

                        break;
                }
            }

            AchatIndisponibleWIP.alpha = 0;
            AchatIndisponibleWIP.DOFade(1f, .5f);
            yield return new WaitForSeconds(2f);
            AchatIndisponibleWIP.DOFade(0f, .5f);
            yield break;
        }
    }
}