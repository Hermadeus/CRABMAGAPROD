using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Passif/AttackSpeed Boost On Other Effect")]
    public class AttackSpeedBoostOnOtherEffect : BasePassifEffect
    {
        public override void PassifEffect(Unit unit)
        {
            unit.StartCoroutine(Effect(unit));
        }

        IEnumerator Effect(Unit unit)
        {
            CrabUnit crabUnit = unit as CrabUnit;

            if (crabUnit.crabFormationReference.haveReceivePassif)
                yield break;

            crabUnit.crabFormationReference.haveReceivePassif = true;

            //Debug.Log("effecttttt");

            if (unit is IBoostSpeedAttackOnOther)
            {
                IBoostSpeedAttackOnOther boostSpeedAttackOnOther = unit as IBoostSpeedAttackOnOther;

                for (int i = 0; i < crabUnit.gameManager.crabUnitOnBattle.Count; i++)
                {
                    if(crabUnit.gameManager.crabUnitOnBattle[i] != null)
                        crabUnit.gameManager.crabUnitOnBattle[i].AttackSpeed /= boostSpeedAttackOnOther.AttackSpeedMultiplier;
                }

                yield return new WaitForSeconds(boostSpeedAttackOnOther.AttackSpeedBoostTimer);

                for (int i = 0; i < crabUnit.gameManager.crabUnitOnBattle.Count; i++)
                {
                    if (crabUnit.gameManager.crabUnitOnBattle[i] != null)
                        crabUnit.gameManager.crabUnitOnBattle[i].AttackSpeed *= boostSpeedAttackOnOther.AttackSpeedMultiplier;
                }
            }


            yield break;
        }
    }
}