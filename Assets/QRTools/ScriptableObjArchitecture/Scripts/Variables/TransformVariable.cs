using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New transform", menuName = "QRTools/Variables/Transform", order = 1)]
    [ExecuteInEditMode]
    public class TransformVariable : Variable<Transform>
    {
        #region Properties & Variables
        public override Transform Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                PlayEvent();
            }
        }

        public Vector3 Position
        {
            get => Value.position;
            set => Value.position = value;
        }

        public Quaternion LocalRotation
        {
            get => Value.localRotation;
            set => Value.localRotation = value;
        }

        public Quaternion Rotation
        {
            get => Value.rotation;
            set => Value.rotation = value;
        }

        public Vector3 LocalEulerAngles
        {
            get => Value.localEulerAngles;
            set => Value.localEulerAngles = value;
        }

        public Vector3 LocalPosition
        {
            get => Value.localPosition;
            set => Value.localPosition = value;
        }

        #endregion

        public Vector3 SetPosition(Vector3 newPos) => Value.position = newPos;
        public Vector3 GetPosition() => Value.position;

        public Vector3 SetLocalPosition(Vector3 newPos) => Value.localPosition = newPos;
        public Vector3 GetLocalPosition() => Value.localPosition;

        public Quaternion SetRotation(Quaternion newRot) => Value.rotation = newRot;
        public Quaternion GetRotation() => Value.rotation;

        public Vector3 SetEulerAngles(Vector3 newRot) => Value.eulerAngles = newRot;
        public Vector3 GetEulerAngles() => Value.eulerAngles;
    }
}