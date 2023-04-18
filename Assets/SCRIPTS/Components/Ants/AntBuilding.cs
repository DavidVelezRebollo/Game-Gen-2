using ANT.Components.HUD;
using ANT.Input;

using UnityEngine;

namespace ANT.Components.Ants {
    [RequireComponent(typeof(BoxCollider2D), typeof(TowerBridgesUI))]
    public abstract class AntBuilding : MonoBehaviour {
        [SerializeField] protected int RequiredAnts;
        
        protected AntsManager AntsManager;
        protected InputManager Input;
        protected TowerBridgesUI Ui;

        private bool _isColliding;

        #region Unity Events

        private void Start() {
            Input = InputManager.Instance;
            AntsManager = AntsManager.Instance;
            Ui = GetComponent<TowerBridgesUI>();
        }

        private void Update() {
            if (!Input.InteractFlag || AntsManager.CurrentAntsCount() < RequiredAnts || !_isColliding) return;

            Build();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.CompareTag("Game/Ant")) return;
            Ui.ShowText(AntsManager.CurrentAntsCount() ,RequiredAnts);
            _isColliding = true;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Game/Ant")) return;
            Ui.HideText();
            _isColliding = false;
        }

        #endregion

        #region Auxiliar Methods

        protected virtual void Build() {
            Debug.Log($"TO DO - Implement build on {gameObject.name}");
        }

        #endregion
    }
}
