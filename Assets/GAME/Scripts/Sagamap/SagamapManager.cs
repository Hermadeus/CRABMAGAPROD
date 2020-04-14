using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Inputs;

namespace CrabMaga
{
    public class SagamapManager : MonoBehaviour
    {
        public List<CastleSagamap> castles = new List<CastleSagamap>();

        public static SagamapManager instance;

        private void Awake()
        {
            instance = this;

            var objs = FindObjectsOfType<CastleSagamap>();
            for (int i = 0; i < objs.Length; i++)
                castles.Add(objs[i]);
        }
    }
}