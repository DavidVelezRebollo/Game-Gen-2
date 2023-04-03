using System;
using ANT.Components.Core;
using ANT.Input;

using UnityEngine;

namespace ANT.Components.HUD {
    public class Pause : MonoBehaviour {
        private InputManager _input;
        private GameManager _gameManager;

        private void Start() {
            _input = InputManager.Instance;
            _gameManager = GameManager.Instance;
        }

        private void Update() {
            if (!_input.PauseFlag) return;
            
            _gameManager.SetState(_gameManager.GamePaused() ? GameStates.Playing : GameStates.Paused);
        }
    }
}
