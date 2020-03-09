using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New bounded int", menuName = "QRTools/Variables/BoundedVariables/Bounded int", order = 1)]
    public class BoundedIntVariable : BoundedVariable<int>
    {
        #region Properties & Variables
        public override int Value
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
                if(onValueMaxReached != null)onValueMaxReached.Raise();
                PlayEvent();
            }
            if (Value < MinValue)
            {
                Value = MinValue;
                if (onValueMinReached != null) onValueMinReached.Raise();
                PlayEvent();
            }
        }
        #endregion
    }
}
