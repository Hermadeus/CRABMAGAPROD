using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public abstract class BasePassifEffect : Behaviour, IPassifBehaviour
    {
        public abstract void PassifEffect(Unit unit);
    }
}