using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Crabarde : CrabUnit
    {
        public override void Init()
        {
            base.Init();

            
            Debug.Log("dpazkjedpajkzpda^zpdjkap 1");

            for (int i = 0; i < AP_GameManager.Instance.crabUnitOnBattle.Count; i++)
            {
                Debug.Log("jdoajzdjadjapzdjkpakjklsjdklajdloksz 2");

                AP_GameManager.Instance.crabUnitOnBattle[i].Speed = AP_GameManager.Instance.crabUnitOnBattle[i].entityData.behaviourSystem.GetSpeed(AP_GameManager.Instance.crabUnitOnBattle[i].entityData.speedEnum) * 1.35f;
                AP_GameManager.Instance.crabUnitOnBattle[i].Damage = AP_GameManager.Instance.crabUnitOnBattle[i].entityData.damage * 1.35f;
            }
        }

        protected override void Death()
        {
            AP_GameManager.Instance.haveAlreadyBarde = CheckBardeOnUnit();

            base.Death();
        }

        public bool CheckBardeOnUnit()
        {
            for (int i = 0; i < AP_GameManager.Instance.crabUnitOnBattle.Count; i++)
            {
                if (AP_GameManager.Instance.crabUnitOnBattle[i] is Crabarde)
                    return true;
            }

            Debug.Log("crabarde effect end");

            return false;
        }
    }
}