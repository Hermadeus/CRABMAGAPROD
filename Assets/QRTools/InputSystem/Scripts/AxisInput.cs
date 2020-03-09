using UnityEngine;
using QRTools.Variables;
using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    [CreateAssetMenu(fileName = "New Axis", menuName = "QRTools/Input/Axis", order = 1)]
    public class AxisInput : InputAction
    {
        #region Properties & Variables        
        [Tooltip("SIMPLE -> Simple Axis, RAW -> Use GetAxisRaw, CLAMPED -> ClampedReturnValue")] [EnumPaging]
        public AxisType axisType;

        [Tooltip("Reference a FloatVariable to shareValue easier")]
        public FloatVariable floatVariableRef;
        [Tooltip("Play an event if value != 0")]
        public GameEvent inputEvent;

        [Tooltip("Float returned")]
        [SerializeField, ReadOnly] private float value;

        public float Value
        {
            get => value;
        }
        #endregion

        #region Public Methods
        public override void Execute()
        {
            ReturnValue();
        }

        public override void Init()
        {
            
        }

        public float ReturnValue()
        {
            if (!isActive) return 0;

            switch (axisType)
            {
                case AxisType.SIMPLE:
                    value = Input.GetAxis(inputName);
                    break;
                case AxisType.RAW:
                    value = Input.GetAxisRaw(inputName);
                    break;
                case AxisType.CLAMPED:
                    value = Input.GetAxis(inputName);
                    break;
            }

            if (floatVariableRef != null)
                floatVariableRef.Value = value;

            if (value != 0 && inputEvent != null)
                inputEvent.Raise();

            return value;
        }
        #endregion

        #region Enum
        public enum AxisType
        {
            SIMPLE,
            RAW,
            CLAMPED
        }
        #endregion
    }
}