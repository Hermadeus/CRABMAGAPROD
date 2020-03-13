using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class SimpleEnemy : Unit
    {
        public override void Push()
        {
            base.Push();
            Destroy(gameObject);
        }
    }
}