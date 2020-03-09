using UnityEngine;
using UnityEngine.Events;

using QRTools.Actions;

namespace QRTools.Events
{
    public class Event_TriggerExit3D : CollisionActions_3D
    {
        private UnityEvent onTriggerExit = default;

        private void OnTriggerExit(Collider other)
        {
            if (!isActive)
                return;

            if (!testWithTag && other == targetCollider)
            {
                onTriggerExit?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && other.tag == tag)
            {
                onTriggerExit?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
