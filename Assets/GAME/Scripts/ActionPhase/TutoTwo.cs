using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.Inputs;
using DG.Tweening;

namespace CrabMaga
{
    public class TutoTwo : MonoBehaviour
    {
        // 1- Crabarde + jeu en pause
        // 2- Dialogue pour les compteur de token
        // 3- Ajout du compteur de pop (dialogue)

        public InputTouch wheelInput;
        public UnitWheel wheel;
        public PlayerData playerData;
        public CrabUnitData craberzerk, crabarde;
        public BoiteDialogue boiteDialogue;
        public Image flecheCompteur, flecheTokens;
        public IA_Manager IA_Manager;
        public CanvasGroup rappelInput;

        private void Awake()
        {
            StartCoroutine(Tuto());
        }

        bool asTap = false;

        public IEnumerator Tuto()
        {
            //1
            wheelInput.isActive = false;
            playerData.entityData_slot01 = craberzerk;
            playerData.entityData_slot02 = crabarde;
            playerData.entityData_slot03 = null;
            playerData.entityData_slot04 = null;
            playerData.leader_slot = null;
            wheel.Init();

            yield return new WaitForSeconds(.2f);
            boiteDialogue.ShowDialogue(
                "Pour prendre l'avantage contre l'ennemi, utilise differentes unites de crabes.",
                "To take the advantage on the enemy, use different crabs units.",
                null
                );
            yield return new WaitForSeconds(.2f);

            wheelInput.isActive = true;

            rappelInput.DOFade(1f, .5f);
            wheel.slot01.onTuto.AddListener(OnOpenning);
            wheel.slot02.onTuto.AddListener(OnOpenning);

            while (asTap != true)
            {
                yield return null;
            }

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();

            yield return new WaitForSeconds(1f);
            for (int i = 0; i < AP_GameManager.Instance.crabUnitOnBattle.Count; i++)
            {
                AP_GameManager.Instance.crabUnitOnBattle[i].inTuto = true;
            }
            ///////////////
            
            //2
            yield return new WaitForSeconds(.5f);
            boiteDialogue.ShowDialogue(
                "Chaque unite formee depense 1 jeton. Ils se rechargent avec le temps.",
                "Each units created spend 1 token. They refill with time.",
                null
                );
            yield return new WaitForSeconds(.5f);
            ShowFleche(flecheTokens);

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            HideFleche(flecheTokens);
            boiteDialogue.Hide();

            //3
            yield return new WaitForSeconds(.5f);
            boiteDialogue.ShowDialogue(
                "Les unites coutent egalement un montant de population.",
                "The units also cost an amount of population.",
                null
                );
            yield return new WaitForSeconds(.2f);
            ShowFleche(flecheCompteur);

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flecheCompteur);

            yield return new WaitForSeconds(.5f);
            boiteDialogue.ShowDialogue(
                "Cette valeur est gardee d'un niveau à l'autre. Surveille-la pour ne pas faire faillite !",
                "This value is kept from a level to an other. Watch it closely to avoid brankruptcy !",
                null
                );


            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();

            yield return new WaitForSeconds(.5f);
            IA_Manager.onGameStart?.Invoke(IA_Manager);

            for (int i = 0; i < AP_GameManager.Instance.crabUnitOnBattle.Count; i++)
            {
                if (AP_GameManager.Instance.crabUnitOnBattle[i] is CrabUnit)
                {
                    AP_GameManager.Instance.crabUnitOnBattle[i].inTuto = false;

                    AP_GameManager.Instance.crabUnitOnBattle[i].Speed = AP_GameManager.Instance.crabUnitOnBattle[i].entityData.baseSpeed;
                }
                Debug.Log(AP_GameManager.Instance.crabUnitOnBattle[i].entityData.baseSpeed);

                //AP_GameManager.Instance.crabUnitOnBattle[i].Speed = AP_GameManager.Instance.crabUnitOnBattle[i].entityData.baseSpeed;
            }

            wheelInput.isActive = true;
        }

        public void OnOpenning()
        {
            rappelInput.DOFade(0, .5f);
            wheel.slot01.onTuto.RemoveListener(OnOpenning);
            wheel.slot02.onTuto.RemoveListener(OnOpenning);
            asTap = true;
            wheelInput.isActive = false;
            Debug.Log("select");
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