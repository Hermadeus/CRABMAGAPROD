using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using QRTools.UI;
using QRTools.Variables;

namespace CrabMaga
{
    public class TogglePerso : UIElement
    {
        public Image inside;

        public BoolVariable state = default;

        public Sprite backgroundTrue, backgroundFalse;
        public Sprite insideTrue, insideFalse;

        public UnityEvent ontrue, onfalse;

        public override void Init()
        {
            base.Init();

            UpdateToggle();
        }

        public void OnClick()
        {
            state.Value = !state.Value;
            UpdateToggle();
        }

        private void UpdateToggle()
        {
            if (state.Value)
            {
                background.sprite = backgroundTrue;
                inside.sprite = insideTrue;
                ontrue.Invoke();
            }
            else
            {
                background.sprite = backgroundFalse;
                inside.sprite = insideFalse;
                onfalse.Invoke();
            }
        }
    }
}