using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class CoquillageARamasser : MonoBehaviour
    {
        public bool isRamasser = false;
        public PlayerData playerData;
        public HeaderMoney headerMoney;

        private void Awake()
        {
            isRamasser = false;
        }

        public void Ramasser()
        {
            if (!isRamasser)
            {
                isRamasser = true;
                playerData.shellMoney += 10;
                headerMoney.UpdateMoney();

                transform.DOShakeRotation(1f).SetEase(Ease.InOutSine);
                GetComponentInChildren<Transform>().DOLocalMoveY(GetComponentInChildren<Transform>().localPosition.y + 5, 2f).SetEase(Ease.InOutCirc);
                transform.DOScale(Vector3.zero, 5f).SetEase(Ease.OutBack);

                Invoke("SetOFF", 5f);
            }
        }

        void SetOFF()
        {
            gameObject.SetActive(false);
        }
    }
}