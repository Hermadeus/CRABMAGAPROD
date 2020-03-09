using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Input/TouchType/Pinch", order = 20)]
    public class Pinch : ScriptableObject, ITouchInput
    {
        public void Execute(InputTouch inputTouch)
        {
            if (inputTouch.Touches.Length > 1)
            {
                inputTouch.touchZeroPrevPos = inputTouch.Touches[0].position - inputTouch.Touches[0].deltaPosition;
                inputTouch.touchOnePrevPos = inputTouch.Touches[1].position - inputTouch.Touches[1].deltaPosition;

                inputTouch.prevMagnitude = (inputTouch.touchZeroPrevPos - inputTouch.touchOnePrevPos).magnitude;
                inputTouch.currentMagnitude = (inputTouch.Touches[0].position - inputTouch.Touches[1].position).magnitude;

                inputTouch.onPinch?.Invoke(inputTouch.pinchValue);
            }
            else
                inputTouch.pinchValue = 0;
        }
    }
}