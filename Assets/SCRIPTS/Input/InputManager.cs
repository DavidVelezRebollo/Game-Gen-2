using UnityEngine;

namespace ANT.Input
{
    public class InputManager : MonoBehaviour {
        public static InputManager Instance;
        private InputActions _input;

        public Vector2 Movement { get; private set; }
        public bool AntSelectFlag { get; private set; }
        public bool PauseFlag { get; private set; }
        public bool RightFlag { get; private set; }
        public bool LeftFlag { get; private set; }

        #region Unity Methods

        private void Awake() {
            if (Instance == null) Instance = this;

            _input = new InputActions();
        }

        private void Update() {
            Movement = _input.InputPlayer.Movement.ReadValue<Vector2>();

            AntSelectFlag = _input.InputPlayer.Change.WasPressedThisFrame();
            PauseFlag = _input.UIInputs.Pause.WasPressedThisFrame();
            RightFlag = _input.InputPlayer.RightSelect.WasPerformedThisFrame();
            LeftFlag = _input.InputPlayer.LeftSelect.WasPerformedThisFrame();
        }

        private void OnEnable() {
            _input.Enable();
        }

        private void OnDisable() {
            _input.Disable();
        }

        #endregion
    }
}
