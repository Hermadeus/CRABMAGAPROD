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

        public Crab GetRandomCrab()
        {
            int x = Random.Range(0, entities.Count);

            Crab c = entities[x].crab;

            if (c == null)
            {
                Crab c_test = null;
                for (int i = 0; i < entities.Count; i++)
                    if (entities[i].crab != null)
                        c_test = entities[i].crab;

                if (c_test == null)
                    return null;
                else
                    GetRandomCrab();
            }
            else
            {
                entities[x].crab = null;
                return c;
            }

            return null;
        }

        public Crab GetNextCrab()
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].crab != null)
                {
                    Crab c = entities[i].crab;
                    entities[i].crab = null;
                    return c;
                }
            }

            return null;
        }

        public Crab PushRandomCrab()
        {
            Crab c = GetNextCrab();

            c.Push();

            return c;
        }

        public int CountOfEntities()
        {
            int x = 0;

            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].crab != null)
                    x++;
            }

            return x;
        }

    }

    [System.Serializable]
    public class EntitiesInUnit
    {
        public Crab crab;

        public Transform transformObject;
    }
}