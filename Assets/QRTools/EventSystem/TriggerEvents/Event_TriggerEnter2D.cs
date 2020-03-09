using UnityEngine;
using UnityEngine.Events;

using QRTools.Actions;

namespace QRTools.Events
{
    public class Event_TriggerEnter2D : CollisionActions_2D
    {
        public UnityEvent onTriggerEnter = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isActive)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                onTriggerEnter?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && collision.tag == tag)
            {
                onTriggerEnter?.Invoke();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}