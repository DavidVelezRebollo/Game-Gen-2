using ANT.Interfaces.Interactable;
using ANT.Components.Audio;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ANT.Classes.Interactables {
    public class Sign : IInteractable {
        private string _header;
        private string _content;
        private Transform _parent;
        private GameObject _ui;
        private float _height;

        public Sign(Transform parent, GameObject ui, string header, string content, float height) {
            _header = header;
            _content = content;
            _parent = parent;
            _ui = ui;
            _height = height;
        }

        public void Interact(InputAction.CallbackContext callback) {
            _ui.transform.SetParent(_parent);
            _ui.transform.localPosition = new Vector3(0, _height, 0);
            _ui.SetActive(true);

            SoundManager.Instance.Play("Sign");
        }

        public void Interact() { }
    }
}