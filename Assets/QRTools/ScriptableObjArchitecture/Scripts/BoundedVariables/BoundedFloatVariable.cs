using UnityEngine;

namespace QRTools.Variables {
    [CreateAssetMenu(fileName = "New bounded float", menuName = "QRTools/Variables/BoundedVariables/Bounded float", order = 0)]
    public class BoundedFloatVariable : BoundedVariable<float>
    {
        #region Properties & Variables
        public override float Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                SetBoundedValue();
            }
        }
        #endregion

        #region Private Methods
        protected override void SetBoundedValue()
        {
            if (Value > MaxValue)
            {
                Value = MaxValue;
                PlayEvent();
                if (onValueMaxReached != null) onValueMaxReached.Raise();
            }
            if (Value < MinValue)
            {
                Value = MinValue;
                PlayEvent();
                if (onValueMinReached != null) onValueMinReached.Raise();
            }
        }
        #endregion
    }
}