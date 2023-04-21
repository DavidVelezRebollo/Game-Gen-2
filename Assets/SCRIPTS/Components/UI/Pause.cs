using System;
using ANT.Components.Core;
using ANT.Input;

using UnityEngine;
using System.Collections.Generic;

namespace ANT.Components.HUD {
    public class Pause : MonoBehaviour {
        [SerializeField] private GameObject PausePanel;
        [SerializeField] private List<GameObject> ObjectsToHide;
        
        private InputManager _input;
        private GameManager _gameManager;

        #region Unity Events

        private void Start() {
            _input = InputManager.Instance;
            _gameManager = GameManager.Instance;
            
            PausePanel.SetActive(false);
        }

        private void Update() {
            if (!_input.PauseFlag()) return;
            
            HandlePause();
        }

        #endregion

        #region Methods

        public void OnContinueButton() {
            _gameManager.SetState(GameStates.Playing);
            
            foreach (GameObject item in ObjectsToHide)
                item.SetActive(true);
            
            PausePanel.SetActive(!PausePanel.activeInHierarchy);
        }

        public void OnSettingsButton() {
            
        }

        public void OnExitButton() {
            
        }

        #endregion

        #region Auxiliar Methods

        private void HandlePause() {
            _gameManager.SetState(_gameManager.GamePaused() ? GameStates.Playing : GameStates.Paused);

            foreach (GameObject item in ObjectsToHide)
                item.SetActive(!item.activeInHierarchy);
            
            PausePanel.SetActive(!PausePanel.activeInHierarchy);
        }

        #endregion
    }
}
