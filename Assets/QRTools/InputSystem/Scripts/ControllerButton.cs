using QRTools.Variables;
using UnityEngine;

namespace QRTools.Inputs {
    [CreateAssetMenu(fileName = "New key", menuName ="QRTools/Input/Controller Key", order = 5)]
    public class ControllerButton: InputAction
    {
        public KeyCode key;
        public GameEvent onRaise;
        public bool isPressed = true;

        public override void Execute()
        {
            if (!isActive)
                return;

            if (Input.GetKeyDown(key))
            {
                onRaise.Raise();
                isPressed = true;
            }
            else
                isPressed = false;
        }

        public override void Init()
        {
            
        }

        public void Remap(KeyCode newKey)
        {
            key = newKey;
        }
    }
}
