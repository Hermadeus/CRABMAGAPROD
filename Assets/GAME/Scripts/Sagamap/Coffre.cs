using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Variables;
using TMPro;
using QRTools.Inputs;
using QRTools.Audio;
using DG.Tweening;

namespace CrabMaga
{
    public class Coffre : MonoBehaviour, Iinteractable
    {
        public float startTimer = 5;
        public float timer = 0;

        public BoolVariable isStart;

        bool isOpen = false;

        public InputTouch coffreTouch;

        public int populationcount;
        public HeaderMoney headerMoney;

        public TextMeshProUGUI timerTxt, populationTxt;

        public AudioEvent son;
        public AudioSource source;

        public PlayerData playerData;

        private void Awake()
        {
            if (!isOpen)
            {
                if (isStart.Value == false)
                {
                    isStart.Value = false;
                    timer = startTimer;
                }
                else
                {
                    timer = startTimer - Time.time;
                }
            }

            populationcount = Mathf.RoundToInt(Random.Range(playerData.maxCrab * 0.1f, playerData.maxCrab * .5f));
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if (timer > 0)
            {
                int min = ((int)timer / 60);
                timerTxt.text = min.ToString() + "min" + System.Math.Round(timer - (min * 60), 2).ToString() + "s";
            }
            else
            {
                timerTxt.text = "Tap to gain " + populationcount + " crabs!";
            }

            if(coffreTouch.objectHit != null)
            {
                Coffre c = coffreTouch.objectHit.GetComponent<Coffre>();
                c.Select();
            }
        }

        public void Select()
        {
            if (timer <= 0 && !isOpen)
            {
                isOpen = true;
                Open();
            }
        }

        public void Open()
        {
            Debug.Log("OPEN LE COFFRE");
            headerMoney.AddCrab(populationcount);
            transform.DOShakeRotation(1f).SetEase(Ease.InOutSine);
            GetComponentInChildren<Transform>().DOLocalMoveY(GetComponentInChildren<Transform>().localPosition.y + 5, 2f).SetEase(Ease.InOutCirc);
            transform.DOScale(Vector3.zero, 5f).SetEase(Ease.OutBack);
            son.Play(source);
        }

        public void Deselect()
        {

        }
    }
}