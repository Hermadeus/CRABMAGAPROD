using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using QRTools.Inputs;

namespace CrabMaga
{
    public class Debugging : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public InputTouch input;

        public UnitWheel unitWheel;

        private void Awake()
        {
            input.onTouchEnter.AddListener(TestClick);
        }

        private void Update()
        {
            text.text = "STATE : " + input.isActive.ToString() +
                "ONCLICK : " + isClicking +
                "Is Init : " + unitWheel.IsInit;
                ;
        }

        bool isClicking = false;

        void TestClick()
        {
            isClicking = true;
        }
    }
}