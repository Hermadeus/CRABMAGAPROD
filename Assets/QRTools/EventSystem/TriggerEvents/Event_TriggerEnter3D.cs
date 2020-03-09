using UnityEngine;
using UnityEngine.Events;

using QRTools.Actions;

namespace QRTools.Events
{
    public class Event_TriggerEnter3D : CollisionActions_3D
    {
        private UnityEvent onTriggerEnter = default;

        private void OnTriggerEnter(Collider other)
        {
            if (!isActive)
                return;

            if (!testWithTag && other == targetCollider)
            {
                onTriggerEnter?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && other.tag == tag)
            {
                onTriggerEnter?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
