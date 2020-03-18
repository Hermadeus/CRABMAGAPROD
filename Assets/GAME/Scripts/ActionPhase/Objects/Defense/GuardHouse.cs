using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    public class GuardHouse : MonoBehaviour
    {
        [SerializeField] bool isOccupy = false;
        public bool IsOccupy
        {
            get => isOccupy;
            set
            {
                isOccupy = value;
            }
        }

        [SerializeField] SimpleEnemy enemyOccupate = default;
        public SimpleEnemy EnemyOccupate
        {
            get => enemyOccupate;
            set
            {
                enemyOccupate = value;
                if (enemyOccupate == null)
                    IsOccupy = false;
                else
                    IsOccupy = true;
            }
        }
    }
}