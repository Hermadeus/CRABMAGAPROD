using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public abstract class StarWinCondition : ScriptableObject
    {
        public abstract bool WinStar(AP_GameManager gm);
    }
}