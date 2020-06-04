using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                "Hold to open gngngn",
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

            while (asTap != true)
            {
                yield return null;
            }

            AP_GameManager.Instance.leaderOnBattle.transform.position = new Vector3(0, AP_GameManager.Instance.leaderOnBattle.transform.position.y, AP_GameManager.Instance.leaderOnBattle.transform.position.z);

            yield return new WaitForSeconds(3f);

            circle.DOScale(Vector3.zero, .5f).SetEase(Ease.InOutSine);
            AP_GameManager.Instance.leaderOnBattle.inTuto = true;

            boiteDialogue.ShowDialogue(
                "blabla tape sur le bidule uesh bg",
                "dazda",
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

        }
    }
}