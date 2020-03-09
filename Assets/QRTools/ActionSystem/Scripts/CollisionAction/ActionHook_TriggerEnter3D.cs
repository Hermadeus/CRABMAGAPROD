using UnityEngine;

namespace QRTools.Actions
{
    public class ActionHook_TriggerEnter3D : CollisionActions_3D
    {
        private Action[] onTriggerEnter_Actions = default;

        private void OnTriggerEnter(Collider other)
        {
            if (!isActive)
                return;

            if (onTriggerEnter_Actions == null)
                return;

            if (!testWithTag && other == targetCollider)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
            else if (testWithTag && other.tag == tag)
            {
                for (int i = 0; i < onTriggerEnter_Actions.Length; i++)
                    onTriggerEnter_Actions[i].Execute();

                if (uniqueCollision)
                    Destroy(this);
            }
        }
    }
}
