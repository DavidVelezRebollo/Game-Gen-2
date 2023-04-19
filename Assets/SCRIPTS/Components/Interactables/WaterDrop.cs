using ANT.Components.Ants;

using UnityEngine;

namespace ANT.Components.Interactables {
    [RequireComponent(typeof(CapsuleCollider2D), typeof(Rigidbody2D))]
    public class WaterDrop : MonoBehaviour {
        [SerializeField] private CapsuleCollider2D Collider;

        private void Start() {
            Collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Game/Ant") || collision.CompareTag("Game/PlayableAnt")) {
                collision.GetComponent<AntComponent>().Die();
            }

            Destroy(gameObject);
        }
    }
}
