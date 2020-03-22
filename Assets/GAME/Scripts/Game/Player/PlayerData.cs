using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools.Utilities;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Player Data")]
    public class PlayerData : ScriptableObject, IResetable
    {
        [BoxGroup("Economy datas")]
        public int money = 0;

        [BoxGroup("Gameplay datas")]
        public EntityData
            entityData_slot01 = default,
            entityData_slot02 = default,
            entityData_slot03 = default,
            entityData_slot04 = default;

        [Button]
        public void ResetObject()
        {
            money = 0;
        }

        [Button]
        void TRICHE()
        {
            money = 500000;
        }
    }
}