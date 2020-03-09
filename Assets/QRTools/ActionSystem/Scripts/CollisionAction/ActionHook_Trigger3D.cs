using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_Trigger3D : CollisionActions_3D
    {
        public Action[]
            onTriggerEnter_Actions,
            onTriggerExit_Actions;

        [HideInInspector]
        public bool
            uniqColEnter = false,
            uniqColExit = false;

        private void OnTriggerEnter(Collider other)
        {
            if (!isActive || uniqColEnter)
                return;

            if (onTriggerEnter_Actions == null)
                return;

            if (!testWithTag && other == targetCollider)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColEnter = true;
            }
            else if(testWithTag && other.tag == tag)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColEnter = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!isActive || uniqColExit)
                return;

            if (onTriggerExit_Actions == null)
                return;

            if (!testWithTag && other == targetCollider)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColExit = true;
            }
            else if (testWithTag && other.tag == tag)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColExit = true;
            }
        }
    }
}
