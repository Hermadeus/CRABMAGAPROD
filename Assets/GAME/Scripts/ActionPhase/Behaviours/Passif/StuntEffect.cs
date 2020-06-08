using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Passif/Stunt Effect")]
    public class StuntEffect : BasePassifEffect
    {
        public float stuntEffect = 1f;

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

                    if (stuntAttacker.AsStunt)
                        yield break;

                    stuntAttacker.AsStunt = true;
                    target.IsStunt = true;
                    target.Stunt();

                    unit.animator.SetTrigger("onUlt");
                    unit.OnPassifFeedback();

                    Debug.Log("stunt" + stuntAttacker.StuntTime);

                    yield return new WaitForSeconds(stuntEffect);

                    target.IsStunt = false;

                    yield break;

                }
            }
            else
                yield break;

        }
    }
}