using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.Inputs;
using DG.Tweening;
using System;

namespace CrabMaga
{
    public class TutoThree : MonoBehaviour
    {
        // 1 - Pause + pas d'ennemis
        // 2 - Roue -> Craberser  + crabarde + crabzilla
        // 3 - Faire apparaitre 2 ennemis 
        // 4 - Boite de dialogue
        // 5 - Hold to open wheel
        // 6 - Surligner la tile avec le general
        // 6 bis - Zone où mettre le general
        // 6 ter - boite de dialogue (voici la place ideal pour le general)
        // 7 - bloquer les autres tiles ??
        // 8 - boite de dialogue competence
        // 9 - fleche vers le token
        // 10 - ennemis

        public BoiteDialogue boiteDialogue;
        public PoolingManager poolingManager;
        public EnemyData enemy;
        public Transform pos01, pos02;
        public PlayerData playerData;
        public UnitWheel wheel;
        public CrabUnitData craberserk, crabarde;
        public LeaderData crabzilla;
        public InputTouch wheelInput;
        public Transform circle;
        public LeaderToken token;
        public CanvasGroup rappelInput;
        public Image flecheToken;
        public Image flecheSagamap;

        private void Awake()
        {
            StartCoroutine(Tuto());
        }

        public IEnumerator Tuto()
        {
            wheelInput.isActive = false;

            playerData.entityData_slot01 = craberserk;
            playerData.entityData_slot02 = crabarde;
            playerData.entityData_slot03 = null;
            playerData.entityData_slot04 = null;

            playerData.leader_slot = crabzilla;

            wheel.Init();

            yield return new WaitForSeconds(2f);

            Enemy e = poolingManager.Pool(pos01.position, enemy.unitType.GetType()) as Enemy;
            Enemy r = poolingManager.Pool(pos02.position, enemy.unitType.GetType()) as Enemy;

            e.inTuto = true;
            r.inTuto = true;

            e.graphics.SetActive(true);
            r.graphics.SetActive(true);

            rappelInput.DOFade(1f, .5f);

            boiteDialogue.ShowDialogue(
                "Lorsqu'un combat devient difficile, tu peux faire appel à un général crabe depuis la roue de formation.",
                "jodajzodj",
                null
                );

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");
                       
            boiteDialogue.Hide();
            rappelInput.DOFade(0f, .5f);

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            yield return new WaitForSeconds(.2f);

            boiteDialogue.Hide();

            circle.DOScale(Vector3.one, .5f).SetEase(Ease.InOutSine);
            wheelInput.isActive = true;

            wheel.slotGeneral.onTuto.AddListener(OnOpenning);

            yield return new WaitForSeconds(.2f);

            while (asTap != true)
            {
                yield return null;
            }

            yield return new WaitForSeconds(.2f);

            AP_GameManager.Instance.leaderOnBattle.transform.position = new Vector3(0, AP_GameManager.Instance.leaderOnBattle.transform.position.y, AP_GameManager.Instance.leaderOnBattle.transform.position.z);

            yield return new WaitForSeconds(7f);

            circle.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutSine);
            AP_GameManager.Instance.leaderOnBattle.inTuto = true;

            boiteDialogue.ShowDialogue(
                "Les généraux possèdent une compétence spéciale très puissante.",
                "dazda",
                null
                );
            ShowFleche(flecheToken);

            yield return new WaitForSeconds(.5f);

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();
            HideFleche(flecheToken);

            yield return new WaitForSeconds(.5f);
            boiteDialogue.ShowDialogue(
                "Attention cependant, tu ne peux appeler le général qu'une fois par niveau. Sa compétence en revanche se recharge avec le temps.",
                "sazsazs",
                null
                );

            token.onTuto.AddListener(OnUlt);

            while (asTape == false)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();

            AP_GameManager.Instance.leaderOnBattle.inTuto = false;
            AP_GameManager.Instance.leaderOnBattle.Speed = AP_GameManager.Instance.leaderOnBattle.entityData.baseSpeed;
            
            yield break;
        }

        bool asTap = false;

        private void OnOpenning()
        {
            asTap = true;
            wheel.slotGeneral.onTuto.RemoveListener(OnOpenning);
            wheelInput.isActive = false;
        }

        bool asTape = false;

        void OnUlt()
        {
            asTape = true;
            token.onTuto.RemoveListener(OnUlt);
            wheelInput.isActive = true;
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

        public void OnWinTuto()
        {
            if (!onWin)
            {
                StartCoroutine(TutoWin());
                onWin = true;
            }
        }

        bool onWin = false;

        public IEnumerator TutoWin()
        {
            yield return new WaitForSeconds(5f);

            boiteDialogue.ShowDialogue(
                "BLABLA ETOILES",
                "sazsazs",
                null
                );

            while (Input.touchCount == 0)
            {
                yield return null;
            }
            Debug.Log("LE JOUEUR A CLIQUER OMG");

            boiteDialogue.Hide();

            yield return new WaitForSeconds(.5f);

            boiteDialogue.ShowDialogue(
                "TU PEUX CLIQUER ICI POUR REVENIR TOUT MOMENT VERS LA SAGAMAP",
                "",
                null
                );

            ShowFleche(flecheSagamap);

            PlayerPrefs.SetFloat("tutoThree", 0);

            yield break;
        }
    }
}