using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using DG.Tweening;

using QRTools.Inputs;

namespace CrabMaga
{
    public class TutoOne : MonoBehaviour
    {
        // 1 - Le jeu commence en pause. V
        // 2 - Une boite de dialogue apparaît (une phrase)
        // 3 - Une flèche indique les HP ennemis
        // 4 - Dès que le joueur tape n'importe où
        // 5 - New boite de dialogue
        // 6 - Flecge qui indique les HP alliés
        // 7 - Dès que le joueur tape n'importe où
        // 8 - New boite de dialogue
        // 9 - Feedback roue des unités
        // 10 - Utilisation de la roue
        // 11 - Petite flèche vers le crabe à selectionner
        // 12 - On fait apparaître 1 ennemis

        public BoiteDialogue boiteDialogue;
        public Image flecheHPHaut, flecheHPBas;
        public CanvasGroup rappelInput;

        public UnitWheel unitWheel;
        public InputTouch wheelInput;

        public IA_Manager IA_Manager;

        private void Awake()
        {
            StartCoroutine(Tuto());
        }

        public IEnumerator Tuto()
        {
            //1
            wheelInput.isActive = false;

            //2
            yield return new WaitForSeconds(.2f);
            boiteDialogue.ShowDialogue(
                "Chaque crabe qui atteint le château ennemi lui retire un point de vie.",
                "Each crab that reaches the castle removes one health point.",
                null
                );

            //3
            yield return new WaitForSeconds(.5f);
            ShowFleche(flecheHPHaut);

            //4
            while(Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            HideFleche(flecheHPHaut);
            boiteDialogue.Hide();
            yield return new WaitForSeconds(.5f);

            //5
            boiteDialogue.ShowDialogue(
                "Mène l'assaut avec tes unités de crabes, tout en défendant ton propre château.",
                "Lead the assault with your crab units, while defending your own castle.",
                null
                );

            //6
            yield return new WaitForSeconds(.5f);
            ShowFleche(flecheHPBas);

            //7
            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            HideFleche(flecheHPBas);
            boiteDialogue.Hide();
            yield return new WaitForSeconds(.5f);

            //8
            boiteDialogue.ShowDialogue(
                "Maintiens pour ouvrir la roue de formation d'unités.",
                "Hold to open the units formation wheel.",
                null
                );

            yield return new WaitForSeconds(.5f);
            rappelInput.DOFade(1f, .2f).SetEase(Ease.InOutSine);
            wheelInput.isActive = true;
            wheelInput.onLongTapEnd.AddListener(OnLongTap);
            unitWheel.slot01.onTuto.AddListener(OnInvoke);

            yield break;
        }

        public void OnLongTap()
        {
            rappelInput.DOFade(0f, .2f).SetEase(Ease.InOutSine);
            wheelInput.onLongTapEnd.RemoveListener(OnLongTap);            
        }

        public void OnInvoke()
        {
            IA_Manager.onGameStart?.Invoke(IA_Manager);
            boiteDialogue.Hide();
            rappelInput.DOFade(0, .2f).SetEase(Ease.InOutSine);
            unitWheel.slot01.onTuto.RemoveListener(OnInvoke);
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
            im.fillAmount = 0f;

            DOTween.To(
                () => im.fillAmount,
                (x) => im.fillAmount = x,
                0f,
                .5f
                ).SetEase(Ease.InOutSine);
        }
    }
}