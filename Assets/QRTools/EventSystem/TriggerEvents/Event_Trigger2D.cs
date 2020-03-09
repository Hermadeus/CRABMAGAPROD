using UnityEngine;
using QRTools.Actions;
using UnityEngine.Events;

namespace QRTools.Events
{
    public class Event_Trigger2D : CollisionActions_2D
    {
        public UnityEvent
            onTriggerEnter,
            onTriggerExit;

        [HideInInspector]
        public bool
            uniqColEnter = false,
            uniqColExit = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isActive || uniqColEnter)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                onTriggerEnter?.Invoke();

                if (uniqueCollision)
                    uniqColEnter = true;
            }
            else if (testWithTag && collision.tag == tag)
            {
                onTriggerEnter.Invoke();

                if (uniqueCollision)
                    uniqColEnter = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isActive || uniqColExit)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                onTriggerExit?.Invoke();

                if (uniqueCollision)
                    uniqColExit = true;
            }
            else if (testWithTag && collision.tag == tag)
            {
                onTriggerExit?.Invoke();

                if (uniqueCollision)
                    uniqColExit = true;
            }
        }
    }
}
