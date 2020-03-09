using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [System.Serializable]
    public class CrabFormation
    {
        public List<EntitiesInUnit> entities = new List<EntitiesInUnit>();

        public EntitiesInUnit GetNextEmptyPosition()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].crab == null)
                    return entities[i];
            }

            return null;
        }
    }

    [System.Serializable]
    public class EntitiesInUnit
    {
        public Crab crab;

        public Transform transformObject;
    }
}