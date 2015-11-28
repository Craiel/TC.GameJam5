﻿namespace Assets.Scripts.InputSystem
{
    using Assets.Scripts.Enums;
    using Assets.Scripts.InputSystem.Contracts;
    using Assets.Scripts.Systems;

    using CarbonCore.Utils.Unity.Logic;

    public class InputHandler : UnitySingletonBehavior<InputHandler>
    {
        private IInputDeviceMapping mapping;
        
        // -------------------------------------------------------------------
        // Public
        // -------------------------------------------------------------------
        public override void Awake()
        {
            this.RegisterInController(SceneController.Instance, SceneRootCategory.System, true);

            this.CreateKeyboardMapping();
        }

        public void Update()
        {
            // Update the mapping
            this.mapping.Update();
        }

        public void Initialize()
        {
        }

        public InputDeviceState GetState(Controls control)
        {
            return this.mapping.GetState(control);
        }

        // -------------------------------------------------------------------
        // Private
        // -------------------------------------------------------------------
        private void CreateKeyboardMapping()
        {
            KeyboardInputDeviceMapping newMapping = new KeyboardInputDeviceMapping();
            newMapping.SetAxis("Submit", Controls.Start);
            newMapping.SetAxis("Cancel", Controls.Exit);
            newMapping.SetAxis("Vertical", Controls.MoveUp);
            newMapping.SetAxis("Vertical", Controls.MoveDown, false);
            newMapping.SetAxis("Horizontal", Controls.MoveLeft, false);
            newMapping.SetAxis("Horizontal", Controls.MoveRight);

            this.mapping = newMapping;
        }
    }
}