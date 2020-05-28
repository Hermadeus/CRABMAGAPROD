using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Utilities;
using QRTools.Utilities.Observer;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Player Data")]
    public class PlayerData : ScriptableObject, IResetable, IObserver, ISavable
    {
        [BoxGroup("Player Preference")]
        [SerializeField] private bool rightHand = true;

        [BoxGroup("Economy datas")]
        private int crabMoney = 0;
        [BoxGroup("Economy datas")]
        public int shellMoney = 0;
        [BoxGroup("Economy datas")]
        public int pearlMoney = 0;

        [BoxGroup("Gameplay datas")]
        public CrabUnitData
            entityData_slot01 = default,
            entityData_slot02 = default,
            entityData_slot03 = default,
            entityData_slot04 = default;

        [BoxGroup("Gameplay datas")]
        public LeaderData leader_slot = default;

        [SerializeField] List<IObservable> observables = new List<IObservable>();
        public List<IObservable> Observables { get => observables; set => observables = value; }


        public bool RightHand { get => rightHand;
            set
            {
                rightHand = value;
                Debug.Log("MODE : " + value);
                ChangeHand();
            }
        }

        public int CrabMoney
        {
            get => crabMoney;
            set
            {
                crabMoney = value;

                if (crabMoney < 0)
                    crabMoney = 0;

                if (crabMoney > maxCrab)
                    crabMoney = maxCrab;
            }
        }

        public int maxCrab = 5000;

        public bool sfxOn = true, musicOn = true;

        [Button]
        public void ResetObject()
        {
            CrabMoney = 10000;
            shellMoney = 600;
            pearlMoney = 0;

            rightHand = true;
            sfxOn = true;
            musicOn = true;
        }

        public void ChangeHand()
        {            
            PersistableSO.Instance.Save();
            UpdateObservable();
        }

        public void UpdateObservable()
        {
            for (int i = 0; i < Observables.Count; i++)
            {
                Observables[i].Notify();
            }
        }

        [Button]
        void TRICHE()
        {
            CrabMoney = 500000;
            shellMoney = 500000;
            pearlMoney = 500000;
        }

        public void Save()
        {
            PlayerPrefExt.SetBool("hand", RightHand);

            PlayerPrefs.SetInt("crabMoney", CrabMoney);
            PlayerPrefs.SetInt("shellMoney", shellMoney);
            PlayerPrefs.SetInt("pearlMoney", pearlMoney);
        }

        public void Load()
        {
            RightHand = PlayerPrefExt.GetBool("hand");

            CrabMoney = PlayerPrefs.GetInt("crabMoney");
            shellMoney = PlayerPrefs.GetInt("shellMoney");
            pearlMoney = PlayerPrefs.GetInt("pearlMoney");
        }
    }
}