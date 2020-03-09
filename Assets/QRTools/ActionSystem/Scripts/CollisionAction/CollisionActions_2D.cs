using UnityEngine;

namespace QRTools.Actions
{
    public class CollisionActions_2D : CollisionActionsBase
    {
        public Collider2D targetCollider = default;

        protected virtual void OnEnable()
        {
            if (targetCollider == null && string.IsNullOrEmpty(tag))
            {
                throw new System.Exception(string.Format("There are no collider in {0}", gameObject.name));
            }
        }
    }
}
