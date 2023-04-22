using ANT.Components.HUD;

using UnityEngine;

namespace ANT.Components.Tutorial {
    [RequireComponent(typeof(BoxCollider2D))]
    public class TutorialTrigger : MonoBehaviour {
        [SerializeField] private string HeaderText;
        [Multiline]
        [SerializeField] private string ContentText;
        [SerializeField] private GameObject UiGameObject;
        [SerializeField] private InteractableUI Ui;
        [SerializeField] private float Height;

        private void Start() {
            UiGameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            ShowTutorial();
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            HideTutorial();
        }

        private void ShowTutorial() {
            UiGameObject.transform.SetParent(transform);
            UiGameObject.transform.localPosition = new Vector3(0, Height);
            Ui.SetText(HeaderText, ContentText);
            UiGameObject.SetActive(true);
        }

        private void HideTutorial() {
            UiGameObject.SetActive(false);
        }
    }
}
