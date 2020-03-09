using UnityEditor;
using UnityEngine;

namespace QRTools.Variables {
    [CreateAssetMenu(fileName = "New bool", menuName = "QRTools/Variables/Bool", order = 2)]
    public class BoolVariable : Variable<bool>
    {
        #region Properties & Variables
        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                PlayEvent();
                PlayEventState(value);
            }
        }

        [Tooltip("Event each time Value change to true")]
        public GameEvent onValueChangeToTrue = default;

        [Tooltip("Event each time Value change to false")]
        public GameEvent onValueChangeToFalse = default;
        
        [HideInInspector] public bool playOnValueChangeToTrue = true;
        [HideInInspector] public bool playOnValueChangeToFalse = true;
        #endregion

        #region Private Methods
        void PlayEventState(bool state)
        {
            if (state && onValueChangeToTrue != null && playOnValueChangeToTrue) onValueChangeToTrue.Raise();
            if (!state && onValueChangeToFalse != null && playOnValueChangeToTrue) onValueChangeToFalse.Raise();
        }
        #endregion
    }
}
