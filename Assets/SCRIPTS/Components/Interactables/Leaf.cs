using ANT.Components.Ants;

using UnityEngine;

namespace ANT.Components.Interactables {
    public class Leaf : MonoBehaviour {
        [SerializeField] private AntTower TowerAttached;
        [SerializeField] private Sprite TowerLeafSprite;

        private SpriteRenderer _renderer;
        private AntsManager _antsManager;
        private Transform _transform;

        private bool _dirtyFlag;

        private void Start() {
            _renderer = GetComponent<SpriteRenderer>();
            _antsManager = AntsManager.Instance;
            _transform = transform;
        }

        private void FixedUpdate() {
            if (TowerAttached.IsInteractable()) return;

            if (!_dirtyFlag) {
                _renderer.sprite = TowerLeafSprite;
                _transform.rotation = Quaternion.identity;
                _dirtyFlag = true;
            }

            float lastAntXPosition = _antsManager.GetAnt(_antsManager.CurrentAntsCount() - 1).GetAntCurrentPosition().x;
            float lastAntYPosition = _antsManager.GetAnt(_antsManager.CurrentAntsCount() - 1).GetAntCurrentPosition().y;

            Vector3 lastAntPosition = new Vector3(lastAntXPosition, lastAntYPosition);

            _transform.position = lastAntPosition + new Vector3(0, 1.5f);
            Debug.Log("Nose");
        }
    }
}
