using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_TriggerExit3D : CollisionActions_3D
    {
        private Action[] onTriggerExit_Actions = default;

        private void OnTriggerExit(Collider other)
        {
            if (!isActive)
                return;

            if (onTriggerExit_Actions == null)
                return;
            
            if (!testWithTag && other == targetCollider)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && other.tag == tag)
            {
                for (int i = 0; i < onTriggerExit_Actions.Length; i++)
                    onTriggerExit_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
