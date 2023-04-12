using System;
using ANT.Shared;
using ANT.Interfaces.Interactable;
using ANT.Classes.Interactables;
using ANT.Input;
using UnityEngine;

namespace ANT.Components.Interactables {
    public class InteractableComponent : MonoBehaviour {
        [SerializeField] private InteractableTypes Type;

        private IInteractable _interactable;
        private InputManager _input;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;
            InitializeInteractable();
        }

        private void OnTriggerStay2D(Collider2D other) {
            if (!_input.InteractFlag) return;
            
            // TO DO - Show the Interactable UI
            _interactable.Interact();
        }

        #endregion
        
        #region Auxiliar Methods

        private void InitializeInteractable() {
            switch (Type) {
                case InteractableTypes.Leaf:
                    _interactable = new Leaf();
                    break;
                case InteractableTypes.Rock:
                    _interactable = new Rock();
                    break;
                default:
                    Debug.LogError("Error at " + gameObject.name);
                    break;
            }
        }
        
        #endregion
    }
}
