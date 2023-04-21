using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ANT.Input
{
    public class InputManager : MonoBehaviour {
        public static InputManager Instance;
        private InputActions _input;

        private Action<InputAction.CallbackContext> _auxAction;

        public Vector2 Movement { get; private set; }

        #region Unity Methods

        private void Awake() {
            if (Instance == null) Instance = this;

            _input = new InputActions();
        }

        private void Update() {
            Movement = _input.InputPlayer.Movement.ReadValue<Vector2>();
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }

        #endregion

        #region Getters

        public bool AntSelectFlag(){ return _input.InputPlayer.Change.WasPressedThisFrame(); }
        public bool RightFlag(){ return _input.InputPlayer.RightSelect.WasPressedThisFrame(); }
        public bool LeftFlag(){ return _input.InputPlayer.LeftSelect.WasPressedThisFrame(); }
        public bool InteractFlag(){ return _input.InputPlayer.Interact.WasPressedThisFrame(); }
        public bool PauseFlag(){ return _input.UIInputs.Pause.WasPressedThisFrame(); }

        #endregion

        #region Methods

        public void SubscribeInteractFlag(Action<InputAction.CallbackContext> action) {
            _input.InputPlayer.Interact.performed += action;
        }

        public void UnsubscribeInteractFlag(Action<InputAction.CallbackContext> action) {
            _input.InputPlayer.Interact.performed -= action;
        }

        #endregion
    }
}
