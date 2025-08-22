using UnityEngine;
using UnityEngine.InputSystem;



    public class Inputs : MonoBehaviour
    {

    public bool IsLeft;
    public float Trigger, Grip;
        public bool
            TriggerTouched,
            ThumbrestTouched,
            PrimaryTouched,
            PrimaryButton,
            SecondaryTouched,
            SecondaryButton,
            ThumbstickTouched;

        public InputActionReference
            TriggerAction,
            TriggerButtonAction,
            TriggerTouchedAction,
            GripAction,
            ThumbrestTouchedAction,
            PrimaryTouchedAction,
            PrimaryButtonAction,
            SecondaryTouchedAction,
            SecondaryButtonAction,
            ThumbstickTouchedAction;

        private void Awake()
        {
            TriggerAction.action.Enable();
            TriggerTouchedAction.action.Enable();
            TriggerButtonAction.action.Enable();
            GripAction.action.Enable();

            TriggerButtonAction.action.performed += OnTriggerButtonChanged;
            TriggerButtonAction.action.canceled += OnTriggerButtonChanged;

            TriggerTouchedAction.action.performed += OnTriggerTouchChanged;
            TriggerTouchedAction.action.canceled += OnTriggerTouchChanged;
        }

    private void Update()
    {
        ReadTrigger();
        ReadGrip();

        if (IsLeft)
            Grip = Mouse.current.leftButton.isPressed ? 1f : Grip;
        else
            Grip = Mouse.current.rightButton.isPressed ? 1f : Grip;
    }


        private void ReadTrigger()
        {
            Trigger = TriggerAction.action.ReadValue<float>();
        }

        private void OnTriggerButtonChanged(InputAction.CallbackContext ctx)
        {
            PrimaryButton = ctx.performed;
        }

        private void OnTriggerTouchChanged(InputAction.CallbackContext ctx)
        {
            TriggerTouched = ctx.performed;
        }

        private void ReadGrip()
        {
            Grip = GripAction.action.ReadValue<float>();
        }

        private void OnThumbrestTouchChanged(InputAction.CallbackContext ctx)
        {
            ThumbrestTouched = ctx.performed;
        }

        private void OnPrimaryTouchChanged(InputAction.CallbackContext ctx)
        {
            PrimaryTouched = ctx.performed;
        }

        private void OnPrimaryButtonChanged(InputAction.CallbackContext ctx)
        {
            PrimaryButton = ctx.performed;
        }

        private void OnSecondaryTouchChanged(InputAction.CallbackContext ctx)
        {
            SecondaryTouched = ctx.performed;
        }

        private void OnSecondaryButtonChanged(InputAction.CallbackContext ctx)
        {
            SecondaryButton = ctx.performed;
        }

        private void OnThumbstickTouchChanged(InputAction.CallbackContext ctx)
        {
            ThumbstickTouched = ctx.performed;
        }
    }
