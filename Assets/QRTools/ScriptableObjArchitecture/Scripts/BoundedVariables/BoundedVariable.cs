using UnityEngine;

namespace QRTools.Variables
{
    public abstract class BoundedVariable<T> : Variable<T>
    {
        #region Properties & Variables
        public override T Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                base.Value = value;
            }
        }

        [Tooltip("Event when value reach minValue")]
        public GameEvent onValueMinReached;

        [Tooltip("Value can't go below minValue")]
        [SerializeField] private T minValue;
        public T MinValue
        {
            get
            {
                return minValue;
            }
            set
            {
                minValue = value;
            }
        }

        [Tooltip("Event when value reach maxValue")]
        public GameEvent onValueMaxReached;

        [Tooltip("value can't go beyond max value")]
        [SerializeField] private T maxValue;
        public T MaxValue
        {
            get
            {
                return maxValue;
            }
            set
            {
                maxValue = value;
            }
        }
        #endregion

        #region Public Methods
        public T SetMinValue(T value) => MinValue = value;
        public T GetMinValue() => MinValue;

        public T SetMaxValue(T value) => MaxValue = value;
        public T GetMaxValue() => MaxValue;
        #endregion

        #region Private Methods
        /// <summary>
        /// Verify if value don't go exceed minValue or maxValue when it's set
        /// </summary>
        protected abstract void SetBoundedValue();
        #endregion
    }
}
