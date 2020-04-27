using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class HandedSwitch : ButtonSwitch
    {
        public PlayerData playerData;

        public override void Init()
        {
            base.Init();

            if (playerData.RightHand)
            {
                TrySelect("droitier");
            }
            else
            {
                TrySelect("gaucher");
            }
        }
    }
}