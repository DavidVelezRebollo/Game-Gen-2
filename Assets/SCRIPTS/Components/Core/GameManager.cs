using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ANT.Components.Core {
    public enum GameStates {
        Menu,
        Paused,
        Playing
    }

    public class GameManager : MonoBehaviour {
        #region Singleton

        public static GameManager Instance;

        private void Awake() {
            if (Instance != null) return;
            Instance = this;

            _state = DebugMode ? GameStates.Playing : GameStates.Menu;

            if (!DebugMode) SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        #endregion

        [SerializeField] private bool DebugMode;

        private GameStates _state;
        public static Action OnScoreChange;

        private static int _score;
        private bool _gameEnd;

        #region Getters & Setters

        public void SetState(GameStates state) { _state = state; }

        public static void AddScore(int score) {
            _score += score;
            OnScoreChange?.Invoke();
        }

        public static int GetScore() {
            return _score;
        }

        public bool GameEnd() { return _gameEnd; }

        #endregion

        #region Methods

        public bool GamePaused() { return _state != GameStates.Playing; }

        public void EndGame() {
            // TODO - End the game
            _gameEnd = true;
            _state = GameStates.Paused;
        }

        public void WinGame() {
            _state = GameStates.Paused;
        }

        #endregion
    }
}
