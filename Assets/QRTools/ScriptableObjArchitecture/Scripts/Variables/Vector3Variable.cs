using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New Vector3", menuName = "QRTools/Variables/Vector3", order = 4)]
    public class Vector3Variable : Variable<Vector3>
    {
        #region Properties & Variables
        public override Vector3 Value
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
        public Vector3 SetValueX(float value) => Value = new Vector3(value, Value.y, Value.z);
        public float GetValueX() => Value.x;

        public Vector3 SetValueY(float value) => Value = new Vector3(Value.x, value, Value.z);
        public float GetValueY() => Value.y;

        public Vector3 SetValueZ(float value) => Value = new Vector3(Value.x, Value.y, value);
        public float GetValueZ() => Value.z;
        #endregion
    }
}
