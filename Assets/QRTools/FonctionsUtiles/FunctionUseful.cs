using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Functions
{
    public static class FunctionsUseful
    {
        public static void Adds<T>(List<T> list, params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                    list.Add(items[i]);
            }
        }
    }
}
