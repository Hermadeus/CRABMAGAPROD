using UnityEngine;
using QRTools.Variables;
using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    [CreateAssetMenu(fileName = "New Button", menuName = "QRTools/Input/Button", order = 0)]
    public class ButtonInput : InputAction
    {
        #region Properties & Variables        
        [Tooltip("DOWN -> When touch is down, UP -> When touch is up, CLICKED -> While Input is pressed, DOUBLE_CLICK -> When touch is down 2 times in a delay")]
        public InputType inputType;
        [Tooltip("Raise an event when isPressed return true")]
        public GameEvent inputEvent;
        [Tooltip("Reference a BoolVariable")]
        public BoolVariable refBoolVariable;

        [Tooltip("Delay between two click if DOUBLE_CLICK"), ShowIf("inputType", InputType.DOUDLE_CLICK)]
        public float doubleClickTime = .2f;
        private float lastClickTime;

        [Tooltip("Delay between each click if REPETING_CLICK"), ShowIf("inputType", InputType.REPETING_CLICK)]
        public float multiClickTime = .4f;
        private float multiClickTimer;

        [Tooltip("Bool returned"), ReadOnly]
        public bool isPressed = false;
        #endregion

        #region Public Methods
        public override void Init()
        {
            this.lastClickTime = Time.time;
        }

        public override void Execute()
        {
            ReturnValue();
        }

        public bool ReturnValue()
        {
            if (!isActive) return false;

            switch (inputType)
            {
                case InputType.DOWN:
                    isPressed = Input.GetButtonDown(inputName);
                    break;
                case InputType.UP:
                    isPressed = Input.GetButtonUp(inputName);
                    break;
                case InputType.CLICKED:
                    isPressed = Input.GetButton(inputName);
                    break;
                case InputType.DOUDLE_CLICK:
                    isPressed = DoubleClick();
                    break;
                case InputType.REPETING_CLICK:
                    isPressed = RepetingClick();
                    break;
            }

            if (isPressed && inputEvent != null)
                inputEvent.Raise();

            if (refBoolVariable != null)
                refBoolVariable.Value = isPressed;

            return isPressed;
        }
        #endregion

        #region Private Methods
        bool RepetingClick()
        {
            float timeSinceLastClick = Time.time - lastClickTime;

            if (Input.GetButtonDown(inputName))
            {
                if (timeSinceLastClick <= 5)
                {
                    multiClickTimer = multiClickTime;
                }

                lastClickTime = Time.time;
            }

            if (multiClickTimer > 0)
            {
                multiClickTimer -= Time.deltaTime;
                return true;
            }
            else
            {
                return false;
            }
        }

        bool DoubleClick()
        {
            if (Input.GetButtonDown(inputName))
            {
                float timeSinceLastClick = Time.time - lastClickTime;

                if (timeSinceLastClick <= doubleClickTime)
                {
                    lastClickTime = Time.time;
                    return true;
                }

                lastClickTime = Time.time;

                return false;
            }

            return false;
        }
        #endregion
    }

    #region Enum
    public enum InputType
    {
        DOWN,
        UP,
        CLICKED,
        DOUDLE_CLICK,
        REPETING_CLICK
    }
    #endregion
}