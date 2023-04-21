using UnityEngine;

namespace ANT.Components.Ants {
    [RequireComponent(typeof(Animator))]
    public class AntAnimatorHandler : MonoBehaviour {
        private Animator _animator;
        private AntComponent _ant;
        
        private float _movementAmount;

        private void Start() {
            _animator = GetComponent<Animator>();
            _ant = GetComponentInParent<AntComponent>();
        }

        private void Update() {
            HandleAntAnimation();
        }

        private void HandleAntAnimation() {
            if (_ant.IsFollowing()) {
                _movementAmount = 1f;
            } else {
                _movementAmount = !_ant.IsAttached() ? Mathf.Abs(_ant.GetVelocity()) : 0;
            }

            _animator.SetFloat("MovementAmount", _movementAmount);
        }
    }
}
