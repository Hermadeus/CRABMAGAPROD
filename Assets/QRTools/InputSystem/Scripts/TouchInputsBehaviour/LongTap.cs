using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Input/TouchType/LongTap", order = 20)]
    public class LongTap : ScriptableObject, ITouchInput
    {
        public void Execute(InputTouch inputTouch)
        {
            switch (inputTouch.Touch.phase)
            {
                case TouchPhase.Began:
                    inputTouch.longTapChargementVar = 0;
                    inputTouch.longTapChargement = 0;
                    inputTouch.asTap = false;
                    break;
                case TouchPhase.Stationary:
                    if(inputTouch.longTapChargementVar < inputTouch.longTapTimer && !inputTouch.asTap)
                    {
                        inputTouch.longTapChargementVar += Time.deltaTime;
                        inputTouch.longTapChargement = inputTouch.longTapChargementVar / inputTouch.longTapTimer;
                        inputTouch.longTapChargement = Mathf.Clamp01(inputTouch.longTapChargement);

                        inputTouch.onLongTap?.Invoke(inputTouch.longTapChargement);

                        if(inputTouch.longTapChargementVar >= inputTouch.longTapTimer)
                        {
                            inputTouch.onLongTapEnd?.Invoke();
                            inputTouch.asTap = true;
                        }
                    }

                    break;
                case TouchPhase.Ended:
                    inputTouch.longTapChargementVar = 0;
                    inputTouch.longTapChargement = 0;
                    inputTouch.asTap = false;
                    break;
            }
        }
    }
}