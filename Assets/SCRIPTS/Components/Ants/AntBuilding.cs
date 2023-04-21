using ANT.Components.HUD;
using ANT.Input;

using UnityEngine;

namespace ANT.Components.Ants {
    [RequireComponent(typeof(BoxCollider2D), typeof(TowerBridgesUI))]
    public abstract class AntBuilding : MonoBehaviour {
        [SerializeField] protected int RequiredAnts;
        [Space(10)]
        [SerializeField] protected SpriteRenderer InteractableRenderer;
        [SerializeField] protected Sprite DefaultSprite;
        [SerializeField] protected Sprite HighlightSprite;
        
        protected AntsManager AntsManager;
        protected InputManager Input;
        protected TowerBridgesUI Ui;

        protected bool Interactable;
        private bool _isColliding;

        #region Unity Events

        private void Start() {
            Input = InputManager.Instance;
            AntsManager = AntsManager.Instance;
            Ui = GetComponent<TowerBridgesUI>();

            Interactable = true;
        }

        private void Update() {
            if (!Input.InteractFlag() || AntsManager.CurrentAntsCount() < RequiredAnts || !_isColliding) return;

            Build();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.CompareTag("Game/PlayableAnt") || !Interactable) return;
            Ui.ShowText(AntsManager.CurrentAntsCount(), RequiredAnts);
            _isColliding = true;
            InteractableRenderer.sprite = HighlightSprite;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Game/PlayableAnt") || !Interactable) return;
            Ui.HideText();
            _isColliding = false;
            InteractableRenderer.sprite = DefaultSprite;
        }

        #endregion

        #region Getters & Setters

        public bool IsInteractable() { return Interactable; }

        #endregion

        #region Auxiliar Methods

        protected virtual void Build() {
            Debug.Log($"TO DO - Implement build on {gameObject.name}");
        }

        protected void Hide() {
            Ui.HideText();
            _isColliding = false;
        }

        #endregion
    }
}
