﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs {
    [CreateAssetMenu(menuName = "QRTools/Input/TouchType/DoubleTap", order = 20)]
    public class DoubleTap : ScriptableObject, ITouchInput
    {
        public void Execute(InputTouch inputTouch)
        {
            Debug.Log(1);

            if (inputTouch.Touch.tapCount == inputTouch.tapCount)
            {
                Debug.Log(2);

                if (!inputTouch.asTap)
                {
                    inputTouch.onDoubleTap?.Invoke();
                    Debug.Log("cheh");
                    inputTouch.asTap = true;
                }
            }
            if (inputTouch.Touch.phase == TouchPhase.Ended)
                inputTouch.asTap = false;
        }
    }
}
