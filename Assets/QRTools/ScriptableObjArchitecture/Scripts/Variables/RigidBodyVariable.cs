using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New Rigidboby Variable", menuName = "QRTools/Variables/RigidBody", order = 100)]
    public class RigidBodyVariable : Variable<Rigidbody>
    {
        public Vector3 Velocity
        {
            get => Value.velocity;
            set => Value.velocity = value;
        }
    }
}
