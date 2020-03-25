using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.Inputs;

namespace CrabMaga
{
    public class LeaderEffectEvent : MonoBehaviour
    {
        public InputTouch leaderEffectInputTouch = default;

        public void EffectLeaderEvent()
        {
            Leader leader = null;

            if(leaderEffectInputTouch.objectHit != null)
                leaderEffectInputTouch.objectHit.transform.parent.TryGetComponent<Leader>(out leader);

            if (leader == null)
                return;

            leader.UsePassif();
        }
    }
}