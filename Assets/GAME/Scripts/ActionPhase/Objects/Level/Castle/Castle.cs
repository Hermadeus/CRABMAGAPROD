using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

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

        [SerializeField] int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;

                healthSlider.value = value;

                healthBarIm.color = gradient.Evaluate(startHealth / value);

                pvText.text = value.ToString() + " HP";

                if (health <= 0)
                    AP_GameManager.Win();
            }
        }
        
        private void Awake()
        {
            healthSlider.maxValue = AP_GameManager.levelData.scoreToReach;
            healthSlider.value = healthSlider.maxValue;
            Health = AP_GameManager.levelData.scoreToReach;
            startHealth = AP_GameManager.levelData.scoreToReach;
            healthBarIm.color = gradient.Evaluate(1);
        }

        public void LosePV()
        {
            Health--;
        }
    }
}