using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New Vector2Int", menuName = "QRTools/Variables/Vector2Int", order = 5)]
    public class Vector2IntVariable : Variable<Vector2Int>
    {
        #region Properties & Variables
        public override Vector2Int Value
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
        public Vector2Int SetValueX(int value) => Value = new Vector2Int(value, Value.y);
        public float GetValueX() => Value.x;

        public Vector2Int SetValueY(int value) => Value = new Vector2Int(Value.x, value);
        public float GetValueY() => Value.y;
        #endregion
    }
}
