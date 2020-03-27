using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

                    #region CASSE
                    //Vector3 to = unit.Target.transform.position * 10;

                    ////Vector3 mMidPosition = laserAttacker.Source.position + ((unit.Target.transform.position - laserAttacker.Source.position) * 0.5F);
                    //Vector3 mMidPosition = (to - laserAttacker.Source.position).normalized / 2f;
                    //Vector3 mDirection = (unit.Target.transform.position - laserAttacker.Source.position).normalized;


                    //laserAttacker.Mid.position = mMidPosition;
                    //laserAttacker.Mid.rotation = Quaternion.LookRotation(mDirection, Vector3.up);

                    //laserAttacker.Laser.transform.position = mMidPosition;
                    //laserAttacker.Laser.transform.rotation = Quaternion.LookRotation(mDirection, Vector3.right) * Quaternion.Euler(90, 0, 0);
                    //laserAttacker.Laser.transform.localScale = new Vector3(
                    //    laserAttacker.LaserSize,
                    //    ((to - laserAttacker.Source.position) * .5F).magnitude,
                    //    laserAttacker.LaserSize
                    //    );
                    #endregion


                    c.LaserTarget = Physics.OverlapCapsule(laserAttacker.StartPos.position, laserAttacker.EndPos.position, laserAttacker.LaserSize, unit.layerMaskTarget);

                    if (normalizedTime > 0 && !firstHit)
                    {
                        InfligeDamage(laserAttacker);
                        firstHit = true;
                    }

                    if (normalizedTime > .25f && !secondHit)
                    {
                        InfligeDamage(laserAttacker);
                        secondHit = true;
                    }

                    if (normalizedTime > .5f && !thirdHit)
                    {
                        InfligeDamage(laserAttacker);
                        thirdHit = true;
                    }

                    if (normalizedTime > .75f && !quadHit)
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