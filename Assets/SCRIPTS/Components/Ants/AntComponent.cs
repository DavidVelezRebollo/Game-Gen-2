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

        private IAnt _ant;
        private AntComponent _attachedAnt;
        private Transform _transform;
        private Color _testColor;
        private LayerMask _ground;
        private RaycastHit2D _groundHit;

        private const float _FOLLOW_THRESHOLD = 1f;
        private float _attachedMovementSpeed;

        private bool _attached;
        private bool _onAir;
        private bool _selected;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;
            _gameManager = GameManager.Instance;

            _ant = InitializeAntType();
            _rb = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
            _transform = transform;
            _ground = LayerMask.GetMask("Game/Ground");

            _testColor = _renderer.color;
        }

        private void FixedUpdate() {
            if (_gameManager.GamePaused()) return;

            _groundHit = Physics2D.Raycast(transform.position, Vector2.down, 1f,_ground);
            _onAir = !_groundHit.collider;

            if (_attached) {
                FollowAttached();
                return;
            }

            Move();
        }

        #endregion

        #region Getters & Setters

        public float GetSpeed() { return Stats.Speed; }

        public void SetAttachedAnt(AntComponent attached) {
            _attachedAnt = attached;
            _attached = attached;
        }
        
        public void SetAttachedSpeed(float speed){ _attachedMovementSpeed = speed; }

        #endregion

        #region Methods

        public void Highlight(Color color) {
            _renderer.color = color;
        }

        public void Dehighlight() {
            _renderer.color = _testColor;
        }

        #endregion

        #region Auxiliar Methods

        private void Move() {
            _rb.velocity = new Vector2(_input.Movement.x * Stats.Speed, _rb.velocity.y);
        }

        private void FollowAttached() {
            if (Vector2.Distance(_transform.position, _attachedAnt._transform.position) <= _FOLLOW_THRESHOLD) return;
            
            Vector2 targetPosition = _attachedAnt._transform.position;
            Vector2 currentPosition = _transform.position;
            float maxDistanceDelta = _attachedMovementSpeed * Time.fixedDeltaTime;
                
            _transform.position = Vector2.MoveTowards(currentPosition, targetPosition, maxDistanceDelta);
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
                default:
                    Debug.LogError("Type " + Stats.Type + " not implemented yet");
                    break;
            }

            return ant;
        }

        #endregion
    }
}
