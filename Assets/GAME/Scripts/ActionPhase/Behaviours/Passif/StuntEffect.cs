using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Passif/Stunt Effect")]
    public class StuntEffect : BasePassifEffect
    {
        public override void PassifEffect(Unit unit)
        {
            unit.StartCoroutine(Effect(unit, unit.Target));
        }

        IEnumerator Effect(Unit unit, Unit target)
        {
            if (target != null)
            {
                if (target is IStuntable && unit is IStuntAttacker)
                {
                    IStuntAttacker stuntAttacker = unit as IStuntAttacker;

                    target.IsStunt = true;
                    target.Stunt();
                    //Debug.Log("stunt" + stuntAttacker.StuntTime);

                    yield return new WaitForSeconds(stuntAttacker.StuntTime);

                    //Debug.Log("end stunt");

                    target.IsStunt = false;

                    yield break;

                }
            }
            else
                yield break;

        }
    }
}