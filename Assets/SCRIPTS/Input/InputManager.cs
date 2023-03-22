using UnityEngine;

namespace ANT.Input
{
    public class InputManager : MonoBehaviour {
        public static InputManager Instance;
        private InputActions _input;

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

        #region Flags

        public bool AntSelectFlag() {
            return _input.InputPlayer.Change.WasPressedThisFrame();
        }

        #endregion
    }
}
