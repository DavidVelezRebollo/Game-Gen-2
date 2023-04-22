using ANT.Input;

using UnityEngine;
using UnityEngine.InputSystem;

namespace ANT.Components.Interactables {
    public class Teleport : MonoBehaviour {
        [SerializeField] private GameObject TeleportPoint;
        [SerializeField] private GameObject Ui;

        private InputManager _input;
        private GameObject _antTeleported;

        private void Start() {
            _input = InputManager.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            Ui.SetActive(true);
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            _input.SubscribeInteractFlag(TeleportAnt);
            _antTeleported = collision.gameObject;
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            _input.UnsubscribeInteractFlag(TeleportAnt);
            Ui.SetActive(false);
        }

        private void TeleportAnt(InputAction.CallbackContext context) {
            _antTeleported.transform.position = TeleportPoint.transform.position;
            _input.UnsubscribeInteractFlag(TeleportAnt);
        }
    }
}
