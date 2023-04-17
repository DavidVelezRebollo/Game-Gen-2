using ANT.Components.HUD;
using ANT.Input;

using UnityEngine;

namespace ANT.Components.Ants {
    [RequireComponent(typeof(BoxCollider2D), typeof(TowerBridgesUI))]
    public class AntTower : MonoBehaviour {
        [SerializeField] private int RequiredAnts;
        
        private InputManager _input;
        private AntsManager _antsManager;
        private TowerBridgesUI _ui;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;
            _antsManager = AntsManager.Instance;
            _ui = GetComponent<TowerBridgesUI>();
        }

        private void Update() {
            if (!_input.InteractFlag || _antsManager.CurrentAntsCount() < RequiredAnts) return;

            BuildTower();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.CompareTag("Game/Ant")) return;
            _ui.ShowText(_antsManager.CurrentAntsCount() ,RequiredAnts);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (!other.CompareTag("Game/Ant")) return;
            _ui.HideText();
        }

        #endregion

        #region Auxiliar Methods

        private void BuildTower() {
            Debug.Log("TO DO - Build Tower");
        }

        #endregion
    }
}
