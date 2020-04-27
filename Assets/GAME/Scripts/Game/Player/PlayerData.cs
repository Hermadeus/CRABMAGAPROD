using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Utilities;
using QRTools.Utilities.Observer;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Player Data")]
    public class PlayerData : ScriptableObject, IResetable, IObserver
    {
        [BoxGroup("Player Preference")]
        [SerializeField] private bool rightHand = true;

        [BoxGroup("Economy datas")]
        public int crabMoney = 0;
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

        public bool sfxOn = true, musicOn = true;

        [Button]
        public void ResetObject()
        {
            crabMoney = 0;
        }

        public void ChangeHand()
        {
            //if (RightHand)
            //    RightHand = true;
            //else
            //    RightHand = false;
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
            crabMoney = 500000;
            shellMoney = 500000;
            pearlMoney = 500000;
        }
    }
}