using UnityEngine.InputSystem;

namespace ANT.Interfaces.Interactable {
    public interface IInteractable {
        public void Interact(InputAction.CallbackContext callback);

        public void Interact();
    }
}
