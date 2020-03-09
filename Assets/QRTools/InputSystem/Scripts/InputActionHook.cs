using UnityEngine;
using Sirenix.OdinInspector;

namespace QRTools.Inputs
{
    public class InputActionHook : MonoBehaviour
    {
        #region Properties & Variables

        [SerializeField, TextArea(3, 5)] private string description = default;

        [BoxGroup("Input Action")]
        public InputAction[] inputs;
        #endregion

        #region Runtime Methods
        /// <summary>
        /// Play all InputAction.Execute in FixedUpdate;
        /// </summary>

        private void Start()
        {

            if (inputs != null)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    inputs[i].Init();
                }
            }
        }

        /// <summary>
        /// Play all InputAction.Execute in Update;
        /// </summary>
        void Update()
        {
            if (inputs == null)
                return;

            for (int i = 0; i < inputs.Length; i++)
            {
                inputs[i].Execute();
            }
        }
        #endregion
    }
}
