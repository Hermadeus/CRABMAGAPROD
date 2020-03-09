using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_Trigger2D : CollisionActions_2D
    {
        public Action[]
            onTriggerEnter_Actions,
            onTriggerExit_Actions;

        [HideInInspector]
        public bool
            uniqColEnter = false,
            uniqColExit = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isActive || uniqColEnter)
                return;

            if (onTriggerEnter_Actions == null)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColEnter = true;
            }
            else if (testWithTag && collision.tag == tag)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColEnter = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isActive || uniqColExit)
                return;

            if (onTriggerExit_Actions == null)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColExit = true;
            }
            else if (testWithTag && collision.tag == tag)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    uniqColExit = true;
            }
        }
    }
}
