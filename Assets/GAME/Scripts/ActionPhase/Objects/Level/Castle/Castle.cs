using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CrabMaga
{
    public class Castle : MonoBehaviour
    {
        public AP_GameManager AP_GameManager = default;

        public Slider healthSlider = default;

        public UnityEvent onMiddlePV = new UnityEvent();
        public UnityEvent onQuartPV = new UnityEvent();

        [SerializeField] int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;

                healthSlider.value = value;

                if (health <= 0)
                    AP_GameManager.Win();
            }
        }
        
        private void Awake()
        {
            healthSlider.maxValue = AP_GameManager.levelData.scoreToReach;
            healthSlider.value = AP_GameManager.levelData.scoreToReach;
        }

        public void LosePV(int value)
        {
            healthSlider.value -= value;
        }
    }
}