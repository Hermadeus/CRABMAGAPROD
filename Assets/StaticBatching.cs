using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class StaticBatching : MonoBehaviour
    {
        private void Awake()
        {
            StaticBatchingUtility.Combine(gameObject);
        }
    }
}