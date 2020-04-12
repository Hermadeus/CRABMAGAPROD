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
        
        private void Awake()
        {
            healthSlider.value = AP_GameManager.levelData.scoreToReach;
        }
    }
}