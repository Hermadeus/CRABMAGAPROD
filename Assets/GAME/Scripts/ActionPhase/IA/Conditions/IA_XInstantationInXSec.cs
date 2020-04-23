using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Conditions/X instantiation In X time")]
    public class IA_XInstantationInXSec : IA_Condition
    {
        public float timeBeforeAnOtherInstantiation = 5f;

        public Coroutine c;

        public override bool Condition(IA_Manager manager)
        {
            if (c != null)
            {
                manager.StopCoroutine(c);
                c = null;
                return true;
            }

            c = manager.StartCoroutine(Test());

            return false;
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(timeBeforeAnOtherInstantiation);
            c = null;
            yield break;
        }
    }
}