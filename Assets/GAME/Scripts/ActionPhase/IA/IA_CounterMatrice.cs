using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Counter Matrice")]
    public class IA_CounterMatrice : SerializedScriptableObject
    {
        public Dictionary<EntityData, EntityData> matriceCounter = new Dictionary<EntityData, EntityData>();

        public EntityData GetCounter(EntityData entityData)
        {
            EntityData e = null;

            matriceCounter.TryGetValue(entityData, out e);

            return e;
        }
    }
}