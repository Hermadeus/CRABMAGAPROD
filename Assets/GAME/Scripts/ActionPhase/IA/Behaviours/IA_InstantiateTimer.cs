﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Behaviour/Instantiate with Timer")]
    public class IA_InstantiateTimer : IA_Behaviour
    {
        public float timer = 5f;

        [Button]
        public override void CallEvent(IA_Manager manager)
        {
            manager.StartCoroutine(TimerCor(manager));
            Debug.Log("start timer");
        }

        public IEnumerator TimerCor(IA_Manager manager)
        {
            if (manager.guardHouseManager.allEmpty)
            {
                Debug.Log("all empty");
                yield break;
            }

            instantiationRule.Instantiation(manager);
            Debug.Log("instantiate");

            yield return new WaitForSeconds(timer);

            manager.StartCoroutine(TimerCor(manager));

            yield break;
        }
    }
}