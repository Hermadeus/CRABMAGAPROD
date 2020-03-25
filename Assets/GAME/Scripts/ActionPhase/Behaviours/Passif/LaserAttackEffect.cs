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
            Debug.Log("LASER");

            unit.StartCoroutine(Laser(unit));
        }

        IEnumerator Laser(Unit unit)
        {
            bool firstHit = false, secondHit = false, thirdHit = false, quadHit = false;


            if (unit is ILaserAttacker)
            {
                ILaserAttacker laserAttacker = unit as ILaserAttacker;

                laserAttacker.ChargeLaser();

                yield return new WaitForSeconds(laserAttacker.LaserChargeTime);

                laserAttacker.StartLaser();

                float duration = 3f; 
                float normalizedTime = 0;
                while (normalizedTime <= 1f)
                {
                    normalizedTime += Time.deltaTime / duration;

                    Vector3 mMidPosition = laserAttacker.Source.position + ((unit.Target.transform.position - laserAttacker.Source.position) * 0.5F);
                    Vector3 mDirection = (unit.Target.transform.position - laserAttacker.Source.position).normalized;

                    Vector3 to = ((unit.Target.transform.position - laserAttacker.Source.position * 10f));

                    laserAttacker.Mid.position = mMidPosition;
                    laserAttacker.Mid.rotation = Quaternion.LookRotation(mDirection, Vector3.up);

                    laserAttacker.Laser.transform.position = mMidPosition;
                    laserAttacker.Laser.transform.rotation = Quaternion.LookRotation(mDirection, Vector3.right) * Quaternion.Euler(90, 0, 0);
                    laserAttacker.Laser.transform.localScale = new Vector3(
                        laserAttacker.LaserSize,
                        ((to - laserAttacker.Source.position) * .5F).magnitude,
                        laserAttacker.LaserSize
                        );


                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(laserAttacker.Source.transform.position, unit.Target.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                    {
                        CrabZilla c = unit as CrabZilla;
                        c.colli = Physics.OverlapCapsule(unit.transform.position, to, laserAttacker.LaserSize, unit.layerMaskTarget);

                        Debug.Log("Did Hit");
                    }

                    if(normalizedTime > 0 && !firstHit)
                    {
                        Debug.Log("f hit");
                        firstHit = true;
                    }

                    if (normalizedTime > .25f && !secondHit)
                    {
                        Debug.Log("second hit");
                        secondHit = true;
                    }

                    if (normalizedTime > .5f && !thirdHit)
                    {
                        Debug.Log("third hit");
                        thirdHit = true;
                    }

                    if (normalizedTime > .75f && !quadHit)
                    {
                        Debug.Log("quad hit");
                        quadHit = true;
                    }

                    yield return null;
                }

                laserAttacker.StopLaser();

                yield break;
            }
        }
    }
}