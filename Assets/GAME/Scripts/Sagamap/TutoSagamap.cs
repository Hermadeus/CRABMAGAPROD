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
        public Image flechePassif;
        public Image flechBoutonAchat, flecheType, flecheBtnAmelioration, flechePremierChateau;

        private void Awake()
        {
            ResetPlayerPref();

            if(PlayerPrefs.GetFloat("firstParty") == 0)
            {
                StartCoroutine(TutoSG());

                playerData.crabMoney = 1000;
                playerData.shellMoney = 0;
                playerData.pearlMoney = 0;

                Debug.Log("C EST TA PREMIERE TIPAR");

                PlayerPrefs.SetFloat("firstParty", 1);
            }
            else if(PlayerPrefs.GetFloat("firstParty") == 1)
            {
                Debug.Log("C EST PAS LA PREMIERE TIPAR");
            }

            if(PlayerPrefs.GetFloat("firstParty") == 3)
            {
                Debug.Log("TutoThree");
            }

            if(PlayerPrefs.GetFloat("thirdParty") == 1)
            {
                if (PlayerPrefs.GetFloat("TutoFinish") == 1)
                    return;
                StartTutoThree();
            }
        }

        [Button]
        public void ResetPlayerPref()
        {
            PlayerPrefs.SetFloat("firstParty", 0);
            PlayerPrefs.SetFloat("thirdParty", 0);
            PlayerPrefs.SetFloat("TutoFinish", 0);
        }

        public void StartTutoFour()
        {
            StartCoroutine(TutoFour());
        }

        IEnumerator TutoFour()
        {
            yield return new WaitForSeconds(1f);

            boiteDialogue.ShowDialogue(
                "Avant chaque niveau, choisis jusqu'a 4 unites et 1 general depuis cette fenêtre.",
                "Before each level, choose up 4 units and 1 general in this panel."
                );

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }

            boiteDialogue.Hide();

            yield break;
        }

        [Button]
        public void StartTutoThree()
        {
            StartCoroutine(TutoThree());
        }

        IEnumerator TutoThree()
        {
            PlayerPrefs.SetFloat("TutoFinish", 1);
                       
            XP.SetValueX(3);
            jauge.Init();

            ShowFleche(flecheJaugeDeCOnquete);


            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "Une fois completee, la jauge gagne un niveau et confere des coquillages.",
                "Once completed, the gauge levels up and grants shells."
                );

            yield return new WaitForSeconds(4f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            HideFleche(flecheJaugeDeCOnquete);
            boiteDialogue.Hide();

            yield return new WaitForSeconds(.5f);
            boiteDialogue.ShowDialogue(
                "Ces coquillages permettent d'acheter des unites et de les ameliorer. Dirige-toi vers le menu armee.",
                "These shells allow to buy and upgrade units. Head towards the army menu."
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

            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "Tu as de quoi acheter une nouvelle unite de crabes. Pour les choisir, compare leurs caracteristiques et effets.",
                "You have enough to buy a new crab's unit. To choose it, compare theirs statistics and effects."
                );
            ShowFleche(flechePassif);
            ShowFleche(flecheBtnAmelioration);

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flechePassif);
            HideFleche(flecheBtnAmelioration);

            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "Les unites et les ennemis ont aussi un type.",
                "Units and enemies also have a type."
                );
            ShowFleche(flecheType);

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            yield return new WaitForSeconds(.5f);

            boiteDialogue.Hide();
            boiteDialogue.ShowDialogue(
                "La <color=red>FORCE</color> bat la <color=#4F7BC4>RESILIENCE</color>, la <color=#4F7BC4>RESILIENCE</color> bat l'<color=green>AGILITE</color> et l'<color=green>AGILITE</color> bat la <color=red>FORCE</color>",
                "<color=red>STRENGHT</color> beats <color=#4F7BC4>RESILIENCE</color>, <color=#4F7BC4>RESILIENCE</color> beats <color=green>AGILITY</color> and <color=green>AGILITY</color> beats <color=red>STRENGHT</color>.");

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flecheType);

            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "Il te reste suffisamment de coquillages pour ameliorer une unité de ton choix.",
                "You have enough shells to level up a unit of your choice."
                );
            ShowFleche(flecheBtnAmelioration);

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flecheBtnAmelioration);

            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "Pense a faire un tour dans le menu armee pour ameliorer tes unites apres avoir complete une jauge de conquete !",
                "Remember to visit the army menu to upgrade your units after completing a conquest's gauge!"
                );

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }

            boiteDialogue.Hide();

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
                "Bienvenue sur Crab'Maga. Clique ici pour demarrer la conquete de la plage !",
                "Welcome to Crab'Maga! Tap here to start the conquest of the beach!"
                );

            ShowFleche(flechePremierChateau);

            yield return new WaitForSeconds(.2f);
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flechePremierChateau);

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