using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.Variables;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class MapCharger : MonoBehaviour
    {
        public FloatVariable mapRotX = default;

        public GameObject[] maps;

        [SerializeField, ReadOnly] private int chunkIndex = 0;

        public int ChunkIndex
        {
            get => chunkIndex;
            set
            {
                chunkIndex = value;
                UpdateChunk();
            }
        }

        void UpdateChunk()
        {
            for (int i = 0; i < maps.Length; i++)
            {
                if (i != chunkIndex && maps[i] != null)
                    maps[i].SetActive(false);
            }

            if (maps[chunkIndex] != null)
                maps[chunkIndex]?.SetActive(true);

            if (chunkIndex + 1 >= maps.Length)
                return;

            if(maps[chunkIndex + 1] != null)
                maps[chunkIndex + 1]?.SetActive(true);
        }
    }
}