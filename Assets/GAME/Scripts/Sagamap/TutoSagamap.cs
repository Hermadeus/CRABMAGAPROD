using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using QRTools.Variables;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class TutoSagamap : MonoBehaviour
    {
        public BoolVariable firstTuto;
        public PlayerData playerData;
        public BoiteDialogue boiteDialogue;
        public Button sagamapBtn, shopBtn, ArmyBtn, OptionBtn;
        public Vector2IntVariable XP;
        public Image flecheJaugeDeCOnquete;
        public JaugeDeConquete jauge;
        public Image flecheMenuArmee;

        private void Awake()
        {
            if(PlayerPrefs.HasKey("firstParty") == false)
            {
                StartCoroutine(TutoSG());

                playerData.crabMoney = 1000;
                playerData.shellMoney = 0;
                playerData.pearlMoney = 0;

                Debug.Log("C EST TA PREMIERE TIPAR");

                PlayerPrefs.SetFloat("firstParty", 0);
            }
            else if(PlayerPrefs.HasKey("firstParty") == true)
            {
                Debug.Log("C EST PAS LA PREMIERE TIPAR");
            }

            if(PlayerPrefs.HasKey("tutoThree") == true)
            {
                Debug.Log("TutoThree");
            }
        }

        [Button]
        public void StartTutoThree()
        {
            StartCoroutine(TutoThree());
        }

        IEnumerator TutoThree()
        {
            boiteDialogue.ShowDialogue(
                "Jauge de conquete",
                "hdaidzhia"
                );

            ShowFleche(flecheJaugeDeCOnquete);

            XP.SetValueX(3);
            jauge.Init();

            yield return new WaitForSeconds(4f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flecheJaugeDeCOnquete);

            yield return new WaitForSeconds(.5f);
            boiteDialogue.ShowDialogue(
                "Click ici pour ouvrir le menu armée",
                ""
                );
            ShowFleche(flecheMenuArmee);

            ArmyBtn.interactable = true;
            ArmyBtn.onClick.AddListener(OnOpenMenuArmee);

            while(onArmyMenu != true)
            {
                yield return null;
            }

            boiteDialogue.Hide();
            HideFleche(flecheMenuArmee);

            Debug.Log("Suiyt tuto");

            yield break;
        }

        bool onArmyMenu = false;

        void OnOpenMenuArmee()
        {
            onArmyMenu = true;
            ArmyBtn.onClick.RemoveListener(OnOpenMenuArmee);
        }

        IEnumerator TutoSG()
        {
            boiteDialogue.ShowDialogue(
                "Bienvenue sur CM",
                "aeeaz"
                );

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();

            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "RDV 1er CHATEAU ",
                "aeeaz"
                );

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();

            yield break;
        }

        public void ShowFleche(Image im)
        {
            im.fillAmount = 0f;

            DOTween.To(
                () => im.fillAmount,
                (x) => im.fillAmount = x,
                1f,
                .5f
                ).SetEase(Ease.InOutSine);
        }

        public void HideFleche(Image im)
        {
            DOTween.To(
                () => im.fillAmount,
                (x) => im.fillAmount = x,
                0f,
                .5f
                ).SetEase(Ease.InOutSine);
        }
    }
}