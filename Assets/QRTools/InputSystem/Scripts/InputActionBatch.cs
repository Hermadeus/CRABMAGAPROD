using Sirenix.OdinInspector;
using UnityEngine;

namespace QRTools.Inputs
{
    [CreateAssetMenu(fileName = "Action Batch", menuName = "QRTools/Input/InputAction Batch", order = 1)]
    public class InputActionBatch : InputAction
    {
        #region Properties & Variables
        public InputAction[] inputActions;
        #endregion

        #region Public Methods
        public override void Execute()
        {
            if (!isActive)
                return;

            if (inputActions == null)
                return;

            for (int i = 0; i < inputActions.Length; i++)
            {
                inputActions[i]?.Execute();
            }
        }

        public override void Init()
        {
            if (inputActions == null)
                return;

            for (int i = 0; i < inputActions.Length; i++)
            {
                inputActions[i]?.Init();
            }
        }
        #endregion
    }
}
