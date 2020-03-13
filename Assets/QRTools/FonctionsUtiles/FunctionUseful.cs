using System.Collections;
using System.Collections.Generic;
using System;

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

        public static void DistanceMinAction(Transform ObjectOne, Transform ObjectTwo, float distanceMin, Action action)
        {
            if (Vector3.Distance(ObjectOne.position, ObjectTwo.position) < distanceMin)
                action.Invoke();
        }

        public static void DistanceMaxAction(Transform ObjectOne, Transform ObjectTwo, float distanceMax, Action action)
        {
            if (Vector3.Distance(ObjectOne.position, ObjectTwo.position) > distanceMax)
                action.Invoke();
        }
    }
}
