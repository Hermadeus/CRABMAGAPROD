using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class Craberserk : CrabUnit
    {
        public SpriteRenderer feedbackSpriteRdr;

        public override void OnPassifFeedback()
        {
            base.OnPassifFeedback();
            
        }

        protected override void Death()
        {
            ClignotementAlphaFeedback(feedbackSpriteRdr);

            foreach (Craberserk c in gameManager.crabUnitOnBattle)
            {
                c.Damage += Damage / 2f;

                Debug.Log("CRABERSER EFFECT");

                passifSound?.Play(audiosource);
                animator.SetTrigger("onUlt");
            }

            base.Death();
        }
    }
}