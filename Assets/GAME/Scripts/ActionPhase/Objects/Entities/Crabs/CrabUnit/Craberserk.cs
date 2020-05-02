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
            ClignotementAlphaFeedback(feedbackSpriteRdr);
        }
    }
}