using ANT.Shared;
using ANT.Interfaces.Interactable;
using ANT.Classes.Interactables;
using ANT.Components.HUD;
using ANT.Input;

using UnityEngine;

namespace ANT.Components.Interactables {
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractableComponent : MonoBehaviour {
        [SerializeField] private InteractableTypes Type;
        [SerializeField] private bool NeedInput;
        [SerializeField] private float DistanceToInteract;
        [Space(10)]
        [SerializeField] private Sprite InitialSprite;
        [SerializeField] private Sprite HighlightSprite;
        [Space(10)]
        [SerializeField] private GameObject UiGameObject;
        [SerializeField] private InteractableUI Ui;
        [SerializeField] private float Height;
        [SerializeField] private string Header;
        [Multiline]
        [SerializeField] private string Content;

        private IInteractable _interactable;
        private SpriteRenderer _renderer;
        private InputManager _input;

        #region Unity Events

        private void Start() {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.sprite = InitialSprite;
            _input = InputManager.Instance;

            InitializeInteractable();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            if (Ui) Ui.SetText(Header, Content);

            _renderer.sprite = HighlightSprite;
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            if(Vector3.Distance(collision.transform.position, transform.position) > DistanceToInteract) {
                if (UiGameObject) UiGameObject.SetActive(false);
                if(NeedInput) _input.UnsubscribeInteractFlag(_interactable.Interact);
                return;
            }

            if (NeedInput) {
                _input.SubscribeInteractFlag(_interactable.Interact);
                return;
            }

            _interactable.Interact();
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (!collision.CompareTag("Game/PlayableAnt")) return;

            _renderer.sprite = InitialSprite;
            if (UiGameObject) UiGameObject.SetActive(false);
            if (NeedInput) _input.UnsubscribeInteractFlag(_interactable.Interact);
        }

        #endregion

        #region Auxiliar Methods

        private void InitializeInteractable() {
            switch (Type) {
                case InteractableTypes.Seed:
                    _interactable = new Seed(gameObject);
                    break;
                case InteractableTypes.Sign:
                    _interactable = new Sign(transform, UiGameObject, Header, Content, Height);
                    break;
                default:
                    Debug.LogError("Error at " + gameObject.name);
                    break;
            }
        }
        
        #endregion
    }
}
