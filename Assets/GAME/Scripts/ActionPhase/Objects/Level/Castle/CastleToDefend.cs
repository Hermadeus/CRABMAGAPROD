using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace CrabMaga
{
    public class CastleToDefend : MonoBehaviour
    {
        public static CastleToDefend Instance;

        public AP_GameManager gameManager;

        public Gradient gradient;
        public Image healthBarIm;
        int startHealth;

        public TextMeshProUGUI pvText;

        [SerializeField] int currentHealth;
        public int CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value;
                healthBar.value = value;

                healthBarIm.color = gradient.Evaluate(startHealth / value);

                pvText.text = value.ToString() + " HP";

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
            startHealth = gameManager.levelData.castleAllyHealth;
            healthBarIm.color = gradient.Evaluate(1);
        }

        private void Start()
        {
        }

        public void LosePV()
        {
            CurrentHealth--;
        }
    }
}