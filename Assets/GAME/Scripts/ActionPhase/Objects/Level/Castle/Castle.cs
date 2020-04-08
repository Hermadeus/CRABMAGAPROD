using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class Castle : MonoBehaviour
    {
        public AP_GameManager AP_GameManager = default;

        public Slider healthSlider = default;

        private void Awake()
        {
            healthSlider.value = AP_GameManager.levelData.scoreToReach;
        }
    }
}