using UnityEngine;

namespace QRTools.Variables
{
    public class OnEnable_AssignTransform : MonoBehaviour
    {
        [Tooltip("Assign a TransformVariable to reference this game object Transform")]
        public TransformVariable transformVariable;

        private void OnEnable()
        {
            transformVariable.Value = this.transform;
            Destroy(this);
        }
    }
}
