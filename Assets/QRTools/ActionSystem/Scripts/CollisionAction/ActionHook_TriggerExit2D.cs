using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_TriggerExit2D : CollisionActions_2D
    {
        private Action[] onTriggerExit_Actions = default;

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!isActive)
                return;

            if (onTriggerExit_Actions == null)
                return;

            if (!testWithTag && collision == targetCollider)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && collision.tag == tag)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
