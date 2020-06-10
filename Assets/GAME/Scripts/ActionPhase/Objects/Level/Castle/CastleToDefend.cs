using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;
using Sirenix.OdinInspector;

using QRTools.Audio;

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
                
                pvText.text = value.ToString() + " HP";

                if(startHealth / value != 0 && value != 0)
                    healthBarIm.color = gradient.Evaluate(1 / (startHealth / value));

                if (value <= 0)
                    gameManager.Lose();
            }
        }

        public Slider healthBar;

        public AudioSource source;
        public AudioEvent breakCastle;

        private void Awake()
        {
            Instance = this;
            startHealth = gameManager.levelData.castleAllyHealth;

            CurrentHealth = gameManager.levelData.castleAllyHealth;
            healthBar.maxValue = CurrentHealth;
            healthBar.value = CurrentHealth;
            healthBarIm.color = gradient.Evaluate(1);
        }

        [Button]
        public void LosePV()
        {
            CurrentHealth--;
            breakCastle.Play(source);
        }
    }
}