using UnityEngine;

namespace QRTools.Variables {
    [CreateAssetMenu(fileName = "New Vector2", menuName = "QRTools/Variables/Vector2", order = 3)]
    public class Vector2Variable : Variable<Vector2>
    {
        #region Properties & Variables
        public override Vector2 Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                PlayEvent();
            }
        }
        #endregion

        #region Public Methods
        public Vector2 SetValueX(float value) => Value = new Vector3(value, Value.y);
        public float GetValueX() => Value.x;

        public Vector2 SetValueY(float value) => Value = new Vector3(Value.x, value);
        public float GetValueY() => Value.y;
        #endregion
    }
}
