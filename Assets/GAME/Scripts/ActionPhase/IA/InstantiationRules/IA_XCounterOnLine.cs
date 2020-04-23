using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Instantiation Rules/X Instantiation On Line")]

    public class IA_XCounterOnLine : IA_InstantationRules
    {
        public IA_CounterEnemyOnLine iA_CounterEnemyOnLine = default;

        public int nbrUnitToInvoke = 2;

        public float timeBetweenEachInstantiation = 2f;

        public override Entity Instantiation(IA_Manager manager)
        {
            manager.StartCoroutine(T(manager));

            return null;
        }

        IEnumerator T(IA_Manager manager)
        {
            for (int i = 0; i < nbrUnitToInvoke; i++)
            {
                iA_CounterEnemyOnLine.Instantiation(manager);

                yield return new WaitForSeconds(timeBetweenEachInstantiation);
            }            

            yield break;
        }
    }
}