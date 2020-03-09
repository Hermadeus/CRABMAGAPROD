using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Functions;
using System.Linq;

namespace QRTools.Inputs
{
    [CreateAssetMenu(fileName = "New Controller Input", menuName = "QRTools/Input/Controller Map", order = 100)]
    public class ControllerInput : InputAction
    {
        [FoldoutGroup("Joysticks")]
        public AxisInput 
            RightJoystick_X = default,
            RightJoystick_Y = default;
        [FoldoutGroup("Joysticks")]
        public ControllerButton RightJoystick_Button = default;
        [FoldoutGroup("Joysticks")]
        public AxisInput 
            LeftJoyStick_X = default,
            LeftJoyStick_Y = default;
        [FoldoutGroup("Joysticks")]
        public ControllerButton LeftJoystick_Button = default;

        [FoldoutGroup("Directional Pad")]
        public AxisInput 
            DirectionalPad_X = default,
            DirectionalPad_Y = default;

        [FoldoutGroup("Buttons")]
        public ControllerButton
            A = default,
            B = default,
            X = default,
            Y = default;

        [FoldoutGroup("Triggers")]
        public ControllerButton
            RightTrigger = default,
            LeftTrigger = default;

        [FoldoutGroup("Bumpers")]
        public AxisInput
            RightBumpers = default,
            LeftBumpers = default;

        [FoldoutGroup("Others")]
        public ControllerButton
            MenuButton = default,
            DisplayButton = default;

        [FoldoutGroup("Debug Values")]
        [ReadOnly]
        [SerializeField] private List<InputAction> Inputs = new List<InputAction>();

        [Button(ButtonSizes.Small)]
        [ButtonGroup]
        void Map()
        {
            Clear();

            FunctionsUseful.Adds<InputAction>(Inputs,
               RightJoystick_X, RightJoystick_Y, RightJoystick_Button,
               LeftJoyStick_X, LeftJoyStick_Y, LeftJoystick_Button,
               DirectionalPad_X, DirectionalPad_Y,
               A, B, X, Y,
               RightTrigger, LeftTrigger,
               RightBumpers, LeftBumpers,
               MenuButton, DisplayButton
               );
        }

        [Button(ButtonSizes.Small)]
        [ButtonGroup()]
        void Clear()
        {
            Inputs.Clear();
        }

        public override void Init()
        {
           
        }

        public override void Execute()
        {
            if (!isActive)
                return;

            for (int i = 0; i < Inputs.Count; i++)
            {
                Inputs[i].Execute();
            }
        }

        public InputAction FindAction(string name)
        {
            InputAction input = null;

            for (int i = 0; i < Inputs.Count; i++)
            {
                if (Inputs[i].inputName == name)
                    input = Inputs[i];
            }

            return input;
        }
    }
}
