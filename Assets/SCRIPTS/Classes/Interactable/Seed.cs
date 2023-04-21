using ANT.Interfaces.Interactable;
using ANT.Components.Core;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ANT.Classes.Interactables {
    public class Seed : IInteractable {
        private GameObject _seedGameObject;
        private int _scoreValue;

        public Seed(GameObject go) {
            _seedGameObject = go;
            _scoreValue = 1;
        }

        public void Interact(InputAction.CallbackContext callback) { }

        public void Interact() {
            Object.Destroy(_seedGameObject);
            GameManager.AddScore(_scoreValue);
        }
    }
}
