using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class CastleToDefend : MonoBehaviour
    {
        public static CastleToDefend Instance;

        public AP_GameManager gameManager;

        [SerializeField] int currentHealth;
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                healthBar.value = value;

                if (value <= 0)
                    gameManager.Lose();
            }
        }

        public Slider healthBar;

        private void Awake()
        {
            Instance = this;

            CurrentHealth = gameManager.levelData.castleAllyHealth;
            healthBar.maxValue = CurrentHealth;
            healthBar.value = CurrentHealth;
        }

        private void Start()
        {
        }

        public void LosePV(int x)
        {
            CurrentHealth -= x;
        }
    }
}