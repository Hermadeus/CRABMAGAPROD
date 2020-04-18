using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QRTools.Inputs
{
    [CreateAssetMenu(menuName = "QRTools/Input/TouchType/Swipe", order = 20)]
    public class Swipe : ScriptableObject, ITouchInput
    {
        public float SWIPE_THRESHOLD = 20f;

        public void Execute(InputTouch inputTouch)
        {
            if (inputTouch.Touch.phase == TouchPhase.Began)
            {
                inputTouch.fingerUp = inputTouch.Touch.position;
                inputTouch.fingerDown = inputTouch.Touch.position;
            }

            //Detects Swipe while finger is still moving
            if (inputTouch.Touch.phase == TouchPhase.Moved)
            {
                if (!inputTouch.detectSwipeOnlyAfterRelease)
                {
                    inputTouch.fingerDown = inputTouch.Touch.position;
                    checkSwipe(inputTouch);
                }
            }

            //Detects swipe after finger is released
            if (inputTouch.Touch.phase == TouchPhase.Ended)
            {
                inputTouch.fingerDown = inputTouch.Touch.position;
                inputTouch.asSwipe = false;

                if(inputTouch.detectSwipeOnlyAfterRelease)
                    checkSwipe(inputTouch);
            }            
        }

        void checkSwipe(InputTouch inputTouch)
        {
            if (inputTouch.asSwipe && !inputTouch.swipeInContinue)
                return;

            //Check if Vertical swipe
            if (verticalMove(inputTouch) > SWIPE_THRESHOLD && verticalMove(inputTouch) > horizontalValMove(inputTouch))
            {
                //Debug.Log("Vertical");
                if (inputTouch.fingerDown.y - inputTouch.fingerUp.y > 0)//up swipe
                {
                    inputTouch.onSwipeUp?.Invoke(Mathf.Abs(inputTouch.fingerDown.y - inputTouch.fingerUp.y));
                    inputTouch.asSwipe = true;
                }
                else if (inputTouch.fingerDown.y - inputTouch.fingerUp.y < 0)//Down swipe
                {
                    inputTouch.onSwipeDown?.Invoke(Mathf.Abs(inputTouch.fingerDown.y - inputTouch.fingerUp.y));
                    inputTouch.asSwipe = true;
                }
                inputTouch.fingerUp = inputTouch.fingerDown;
            }

            //Check if Horizontal swipe
            else if (horizontalValMove(inputTouch) > SWIPE_THRESHOLD && horizontalValMove(inputTouch) > verticalMove(inputTouch))
            {
                //Debug.Log("Horizontal");
                if (inputTouch.fingerDown.x - inputTouch.fingerUp.x > 0)//Right swipe
                {
                    inputTouch.onSwipeRight?.Invoke(Mathf.Abs(inputTouch.fingerDown.x - inputTouch.fingerUp.x));
                    inputTouch.asSwipe = true;
                }
                else if (inputTouch.fingerDown.x - inputTouch.fingerUp.x < 0)//Left swipe
                {
                    inputTouch.onSwipeLeft?.Invoke(Mathf.Abs(inputTouch.fingerDown.x - inputTouch.fingerUp.x));
                    inputTouch.asSwipe = true;
                }
                inputTouch.fingerUp = inputTouch.fingerDown;
            }
        }

        float verticalMove(InputTouch inputTouch)
        {
            return Mathf.Abs(inputTouch.fingerDown.y - inputTouch.fingerUp.y);
        }

        float horizontalValMove(InputTouch inputTouch)
        {
            return Mathf.Abs(inputTouch.fingerDown.x - inputTouch.fingerUp.x);
        }
    }
}