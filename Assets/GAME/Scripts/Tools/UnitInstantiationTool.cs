using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class UnitInstantiationTool : MonoBehaviour
    {
        public Entity refEntityPrefab = default;

        [Button]
        public void InstantiateUnit(int nbr)
        {
            for (int i = 0; i < nbr; i++)
            {
                var e = Instantiate(refEntityPrefab,
                    new Vector3(-100, 0, -100),
                    Quaternion.identity,
                    this.transform);

                e.enabled = false;
                e.InitButton();
            }

            PoolingManager pm = FindObjectOfType<PoolingManager>();
            pm.GetAllIPoolable();
        }
    }
}