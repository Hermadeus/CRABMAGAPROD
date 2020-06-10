using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

using QRTools.Audio;

namespace CrabMaga
{
    public class Castle : MonoBehaviour
    {
        public AP_GameManager AP_GameManager = default;

        public Slider healthSlider = default;

        public UnityEvent onMiddlePV = new UnityEvent();
        public UnityEvent onQuartPV = new UnityEvent();

        public Gradient gradient;
        public Image healthBarIm;
        int startHealth;

        public TextMeshProUGUI pvText;
        public AudioSource source;

        [SerializeField] int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;

                healthSlider.value = value;

                if (startHealth / value != 0 && value != 0)
                {
                    healthBarIm.color = gradient.Evaluate(1 / (startHealth / value));
                }

                pvText.text = value.ToString() + " HP";

                if (health <= 0)
                    AP_GameManager.Win();
            }
        }

        public AudioEvent breakSound;
        
        private void Awake()
        {
            startHealth = AP_GameManager.levelData.scoreToReach;

            healthSlider.maxValue = AP_GameManager.levelData.scoreToReach;
            healthSlider.value = healthSlider.maxValue;
            Health = AP_GameManager.levelData.scoreToReach;
            healthBarIm.color = gradient.Evaluate(1);
        }

        public void LosePV()
        {
            Health--;
            breakSound?.Play(source);
        }
    }
}