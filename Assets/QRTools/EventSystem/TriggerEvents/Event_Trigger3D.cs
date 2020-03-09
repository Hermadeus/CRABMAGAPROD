using UnityEngine;
using UnityEngine.Events;

using QRTools.Actions;

namespace QRTools.Events
{
    public class Event_Trigger3D : CollisionActions_3D
    {
        public UnityEvent
            onTriggerEnter,
            onTriggerExit;

        [HideInInspector]
        public bool
            uniqColEnter = false,
            uniqColExit = false;

        private void OnTriggerEnter(Collider collision)
        {
            if (!isActive || uniqColEnter)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                Debug.Log(1);

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

        private void OnTriggerExit(Collider collision)
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
