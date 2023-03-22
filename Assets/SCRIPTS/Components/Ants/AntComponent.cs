using ANT.Shared;
using ANT.Input;
using ANT.Interfaces.Ant;
using ANT.Classes.Ants;

using UnityEngine;

namespace ANT.Components.Ants
{
    [RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
    public class AntComponent : MonoBehaviour {
        [SerializeField] private AntStats Stats;

        private Rigidbody2D _rb;
        private SpriteRenderer _renderer;
        private InputManager _input;

        private IAnt _ant;
        private AntComponent _attachedAnt;
        private bool _attached;

        private bool _selected;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;

            _ant = InitializeAntType();
            _rb = GetComponent<Rigidbody2D>();
            _renderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void FixedUpdate() {
            if (_attached) { 
                FollowAttached();
                return; 
            }

            Move();
        }

        #endregion

        #region Getters & Setters

        public IAnt GetAnt() { return _ant; }

        public bool IsAttached() { return _attached; }

        public void Attach() { _attached = true; }

        public void Deattach() { _attached = false; }

        public void SetAttachedAnt(AntComponent attached) { _attachedAnt = attached; }

        #endregion

        #region Auxiliar Methods

        private void Move() {
            _rb.MovePosition(_rb.position + (Stats.Speed * Time.fixedDeltaTime * _input.Movement));
        }

        private void FollowAttached() {
            _rb.MovePosition(_rb.position + (_attachedAnt.Stats.Speed * Time.fixedDeltaTime * _input.Movement));
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
