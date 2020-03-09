using UnityEngine;
using UnityEngine.Events;

using QRTools.Actions;

namespace QRTools.Events
{
    public class Event_TriggerExit2D : CollisionActions_2D
    {
        private UnityEvent onTriggerExit = default;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isActive)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                onTriggerExit?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && collision.tag == tag)
            {
                onTriggerExit?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
