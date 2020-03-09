using UnityEngine;

namespace QRTools.Actions
{
    public class CollisionActionsBase : MonoBehaviour
    {
        [SerializeField] [TextArea(2,4)] private string description;
        public bool isActive = true;
        public bool testWithTag = false;
        public new string tag = "";
        public bool uniqueCollision = false;
    }

    public enum ColliderTypes2D
    {
        COLLIDER_2D,
        COLLIDERS_2D
    }

    public enum ColliderType3D
    {
        COLLIDER,
        COLLIDERS
    }
}
