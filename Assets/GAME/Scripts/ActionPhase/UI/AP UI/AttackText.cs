using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CrabMaga
{
    public class AttackText : MonoBehaviour
    {
        public LanguageManager languageManager = default;
        public TextMeshProUGUI text;
        public AP_GameManager gameManager;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
            text.text = gameManager.levelData.levelName.GetCurrentText(languageManager.LanguageEnum);
        }
    }
}