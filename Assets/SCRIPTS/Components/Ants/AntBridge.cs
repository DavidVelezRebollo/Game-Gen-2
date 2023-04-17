using ANT.Components.HUD;

using UnityEngine;

namespace ANT.Components.Ants {
    [RequireComponent(typeof(BoxCollider2D), typeof(TowerBridgesUI))]
    public class AntBridge : MonoBehaviour {
        [SerializeField] private int RequiredAnts;

        private BoxCollider2D _collider;

        private void Start() {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void Update() {
            
        }
    }
}
