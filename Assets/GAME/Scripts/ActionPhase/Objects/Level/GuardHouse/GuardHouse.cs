using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class GuardHouse : MonoBehaviour
    {
        public bool isOccupy = false;
        public int lineIndex = 0;

        void InitLineIndex()
        {
            lineIndex = Mathf.RoundToInt(transform.position.x);
        }
    }
}