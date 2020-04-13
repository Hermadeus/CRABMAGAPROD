using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace CrabMaga
{
    public class AnimEvent : MonoBehaviour
    {
        public UnityEvent evnt = new UnityEvent();

        public Animator[] FX_Attack = default;

        public void PlayEvent() => evnt.Invoke();

        public void PlayAnimFX()
        {
            for (int i = 0; i < FX_Attack.Length; i++)
            {
                FX_Attack[i].SetTrigger("onAttack");
            }
        }
    }
}