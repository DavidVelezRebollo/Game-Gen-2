using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ANT.Input
{
    public class InputManager : MonoBehaviour {
        public static InputManager Instance;
        private InputActions _input;

        private Action<InputAction.CallbackContext> auxAction;

        public Vector2 Movement { get; private set; }
        public bool AntSelectFlag { get; private set; }
        public bool PauseFlag { get; private set; }
        public bool RightFlag { get; private set; }
        public bool LeftFlag { get; private set; }
        public bool InteractFlag { get; private set; }

        #region Unity Methods

        private void Awake() {
            if (Instance == null) Instance = this;

            _input = new InputActions();
        }

        private void Update() {
            Movement = _input.InputPlayer.Movement.ReadValue<Vector2>();

            ManageFlags();
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }

        #endregion

        #region Methods

        public void SubscribeInteractFlag(Action<InputAction.CallbackContext> action) {
            _input.InputPlayer.Interact.performed += action;
        }

        public void UnsubscribeInteractFlag(Action<InputAction.CallbackContext> action) {
            _input.InputPlayer.Interact.performed -= action;
        }

        #endregion

        #region Auxiliar Methods

        private void ManageFlags() {
            AntSelectFlag = _input.InputPlayer.Change.WasPerformedThisFrame();
            PauseFlag = _input.UIInputs.Pause.WasPerformedThisFrame();
            RightFlag = _input.InputPlayer.RightSelect.WasPerformedThisFrame();
            LeftFlag = _input.InputPlayer.LeftSelect.WasPerformedThisFrame();
            InteractFlag = _input.InputPlayer.Interact.WasPerformedThisFrame();
        }

        #endregion
    }
}
