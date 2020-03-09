using UnityEngine;

namespace QRTools.Variables
{
    public class OnEnable_AssingGameObject : MonoBehaviour
    {
        [Tooltip("Assign a TransformVariable to reference this game object Transform")]
        public GameObjectVariable gameObjectVariable;

        private void OnEnable()
        {
            gameObjectVariable.Value = this.gameObject;
            Destroy(this);
        }
    }
}
