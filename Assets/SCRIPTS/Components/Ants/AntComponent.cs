using System;
using ANT.Shared;
using ANT.Input;
using ANT.Interfaces.Ant;
using ANT.Classes.Ants;
using ANT.Components.Core;

using UnityEngine;

namespace ANT.Components.Ants
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class AntComponent : MonoBehaviour {
        [SerializeField] private AntStats Stats;

        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;
        private InputManager _input;
        private GameManager _gameManager;
        private AntsManager _antsManager;

        private IAnt _ant;
        private AntComponent _attachedAnt;
        private Transform _transform;
        private LayerMask _ground;
        private RaycastHit2D _groundHit;
        private BoxCollider2D _collider;

        private const float _FOLLOW_THRESHOLD = 3.5f;
        private float _attachedMovementSpeed;
        private float _direction; // -1: Left, 1: Right

        private bool _attached;
        private bool _playable;
        private bool _onTower;
        private bool _onBridge;
        private bool _following;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;
            _gameManager = GameManager.Instance;
            _antsManager = AntsManager.Instance;

            _ant = InitializeAntType();
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _transform = transform;
            _ground = LayerMask.GetMask("Game/Ground");

            if (_playable) return;
            
            _collider.isTrigger = true;
            _rb.gravityScale = 0;
        }

        private void Update() {
            if(!_attachedAnt) _renderer.flipX = _direction == -1 ? true : false;
        }

        private void FixedUpdate() {
            if (_gameManager.GamePaused() || !_playable || _onBridge) return;

            if (_attached) {
                FollowAttached();
                return;
            }

            Move();
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.CompareTag("Game/Ant")) return;
            AntComponent ant = col.GetComponent<AntComponent>();
            if (ant._playable) return;

            ant._collider.isTrigger = false;
            ant._rb.gravityScale = 1;
            
            _antsManager.AddAnt(ant);
        }

        #endregion

        #region Getters & Setters
        
        public Vector3 GetAntCurrentPosition() { return transform.position; }

        public float GetSpeed() { return Stats.Speed; }

        public float GetAntDirection() { return _direction; }

        public float GetVelocity() { return _rb.velocity.x; }

        public bool IsFollowing() { return _following; }

        public bool IsAttached() { return _attached; }
        
        public void SetAntLayer(LayerMask layer) { gameObject.layer = layer; }

        public void SetAntPosition(Vector3 position) { _transform.position = position; }

        public void SetAttachedAnt(AntComponent attached) {
            _attachedAnt = attached;
            _attached = attached;
            _playable = true;
        }

        public void SetAttachedSpeed(float speed) { _attachedMovementSpeed = speed; }
        
        public void SetPlayableState(bool playable) { _playable = playable; }

        #endregion

        #region Methods

        public void Die() {
            _gameManager.EndGame();
        }

        public void Highlight(Color color) {
            _renderer.color = color;
        }

        public void Dehighlight() {
            _renderer.color = Color.white;
        }

        public void BuildTower(Vector3 firstAntPosition, float offset) {
            _transform.position = firstAntPosition + new Vector3(0, offset);
            _rb.gravityScale = 0;
            _onTower = true;
        }

        public void BuildBridge(Vector3 firstAntPosition, float offset) {
            _transform.position = firstAntPosition + new Vector3(offset, 0);
            _rb.gravityScale = 0;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
            _onBridge = true;
            gameObject.layer = 7;
        }

        #endregion

        #region Auxiliar Methods

        private void Move() {
            _rb.velocity = new Vector2(_input.Movement.x * Stats.Speed, _rb.velocity.y);
            if(_input.Movement.x != 0) _direction = _input.Movement.x;
        }

        private void FollowAttached() {
            if (Vector2.Distance(_rb.position, _attachedAnt._rb.position) <= _FOLLOW_THRESHOLD) {
                _following = false;
                return;
            }
 
            Vector2 targetPosition = _attachedAnt._transform.position;
            Vector2 currentPosition = _transform.position;
            float maxDistanceDelta = _attachedMovementSpeed * Time.fixedDeltaTime;

            if (!_onTower) {
                _transform.position = Vector2.MoveTowards(currentPosition, targetPosition, maxDistanceDelta);
                _following = true;
            } else
                _transform.position = new Vector2(_antsManager.GetAnt(0)._transform.position.x, currentPosition.y);

            _renderer.flipX = _attachedAnt._direction == -1 ? true : false;
            _direction = _attachedAnt._direction;
        }

        private IAnt InitializeAntType() {
            IAnt ant = null;

            switch (Stats.Type) {
                case AntTypes.SmallAnt:
                    ant = new SmallAnt();
                    break;
                case AntTypes.BigAnt:
                    ant = new BigAnt();
                    break;
                case AntTypes.RedAnt:
                    ant = new RedAnt();
                    break;
                default:
                    Debug.LogError("Type " + Stats.Type + " not implemented yet");
                    break;
            }

            return ant;
        }

        #endregion
    }
}
