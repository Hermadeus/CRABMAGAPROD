using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEngine.UI;

using TMPro;

namespace CrabMaga
{
    public class UIMenuArmyStat : MonoBehaviour
    {
        public GameObject upgradeIcon;
        public TextMeshProUGUI value;
        public Image logo;

        public void SetOff() => gameObject.SetActive(false);
        public void SetOn() => gameObject.SetActive(true);
    }
}