using UnityEngine;

namespace QRTools.Variables {
    [CreateAssetMenu(fileName = "New Vector3Int", menuName = "QRTools/Variables/Vector3Int", order = 6)]
    public class Vector3IntVariable : Variable<Vector3Int>
    {
        #region Properties & Variables
        public override Vector3Int Value
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
        public Vector3Int SetValueX(int value) => Value = new Vector3Int(value, Value.y, Value.z);
        public float GetValueX() => Value.x;

        public Vector3Int SetValueY(int value) => Value = new Vector3Int(Value.x, value, Value.z);
        public float GetValueY() => Value.y;

        public Vector3Int SetValueZ(int value) => Value = new Vector3Int(Value.x, Value.y, value);
        public float GetValueZ() => Value.z;
        #endregion
    }
}
