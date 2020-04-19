using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Passif/Laser Attack")]
    public class LaserAttackEffect : BasePassifEffect
    {
        public override void PassifEffect(Unit unit)
        {
            unit.StartCoroutine(Laser(unit));
        }

        IEnumerator Laser(Unit unit)
        {
            bool firstHit = false, secondHit = false, thirdHit = false, quadHit = false;
            CrabZilla c = unit as CrabZilla;
            ILaserAttacker laserAttacker = unit as ILaserAttacker;

            if (unit is ILaserAttacker)
            {
                laserAttacker.ChargeLaser();

                yield return new WaitForSeconds(laserAttacker.LaserChargeTime);

                laserAttacker.StartLaser();

                float duration = 3f; 
                float normalizedTime = 0;
                while (normalizedTime <= 1f)
                {
                    normalizedTime += Time.deltaTime / duration;

                    unit.MovementBehaviourEnum = MovementBehaviourEnum.NULL_MOVEMENT;

                    unit.rotationTween.Kill();
                    unit.transform.DOLookAt(new Vector3(unit.transform.position.x, 0, 1000), .5f);

                    c.LaserTarget = Physics.OverlapCapsule(laserAttacker.StartPos.position, laserAttacker.EndPos.position, laserAttacker.LaserSize, unit.layerMaskTarget);

                    if (normalizedTime > .2f && !firstHit)
                    {
                        InfligeDamage(laserAttacker);
                        firstHit = true;
                    }

                    if (normalizedTime > .4f && !secondHit)
                    {
                        InfligeDamage(laserAttacker);
                        secondHit = true;
                    }

                    if (normalizedTime > .6f && !thirdHit)
                    {
                        InfligeDamage(laserAttacker);
                        thirdHit = true;
                    }

                    if (normalizedTime > .8f && !quadHit)
                    {
                        InfligeDamage(laserAttacker);
                        quadHit = true;
                    }

                    yield return null;
                }

                laserAttacker.StopLaser();

                yield break;
            }
        }

        void InfligeDamage(ILaserAttacker laserAttacker)
        {
            for (int i = 0; i < laserAttacker.LaserTarget.Length; i++)
            {
                IAttackReceiver attackReceiver = laserAttacker.LaserTarget[i].GetComponentInParent<Unit>() as IAttackReceiver;
                attackReceiver.ReceiveAttack(laserAttacker as Unit, laserAttacker.LaserDamage / 4f);
                //Debug.Log(((Unit)attackReceiver).name + " + " + laserAttacker.LaserDamage / 4f);
            }
        }
    }
}