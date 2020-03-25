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
        public bool rightHand = true;        

        [BoxGroup("Economy datas")]
        public int money = 0;

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

        [Button]
        public void ResetObject()
        {
            money = 0;
        }

        [Button]
        public void ChangeHand(bool right)
        {
            if (right)
                rightHand = true;
            else
                rightHand = false;

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
            money = 500000;
        }
    }
}