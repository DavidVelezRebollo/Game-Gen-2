using ANT.Components.HUD;

using UnityEngine;

namespace ANT.Components.Interactables {
    [RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
    public class Sign : MonoBehaviour {
        [SerializeField] private string Header;
        [Multiline]
        [SerializeField] private string Content;
        [Space(20)]
        [SerializeField] private Sprite InitialSprite;
        [SerializeField] private Sprite HighlightSprite;
        [SerializeField] private GameObject SignText;
        [SerializeField] private InteractableUI UI;

        private BoxCollider2D _collider2D;
        private SpriteRenderer _renderer;

        private void Start() {
            _collider2D = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();

            _collider2D.isTrigger = true;
            _renderer.sprite = InitialSprite;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            UI.SetText(Header, Content);
            _renderer.sprite = HighlightSprite;
            SignText.transform.localPosition = new Vector3(0, 2, 0);
            SignText.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            SignText.SetActive(false);
            _renderer.sprite = InitialSprite;
        }
    }
}
