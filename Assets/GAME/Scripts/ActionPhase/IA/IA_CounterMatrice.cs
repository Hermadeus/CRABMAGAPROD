using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/IA/Counter Matrice")]
    public class IA_CounterMatrice : SerializedScriptableObject
    {
        [InfoBox("METTRE EN POSITION 0 DE CHAQUE TABLEAU LE COUNTER ABSOLU")]
        public Dictionary<EntityData, EntityData[]> matriceCounter = new Dictionary<EntityData, EntityData[]>();

        public EntityData GetBestCounter(EntityData entityData)
        {
            EntityData[] e = null;

            matriceCounter.TryGetValue(entityData, out e);

            return e[0];
        }

        public EntityData GetRandomCounter(EntityData entityData)
        {
            EntityData[] e = null;

            matriceCounter.TryGetValue(entityData, out e);

            int x = Random.Range(0, e.Length + 1);

            return e[x];
        }

        public EntityData[] GetRandomCounter(EntityData entityData, int count)
        {
            EntityData[] e = null;
            EntityData[] r = new EntityData[count];

            matriceCounter.TryGetValue(entityData, out e);

            r[0] = e[0];
            r[1] = e[1];

            //int[] c = new int[count];

            //for (int i = 0; i < count; i++)
            //    r[i] = e[Random.Range(0, e.Length)];

            return r;
        }
    }
}