using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_TriggerEnter2D : CollisionActions_2D
    {
        public Action[] onTriggerEnter_Actions = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isActive)
                return;

            if (onTriggerEnter_Actions == null)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && collision.tag == tag)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
