using System;
using UnityEngine;

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
        }

        #endregion

        [SerializeField] private bool DebugMode;

        private GameStates _state;
        public static Action OnScoreChange;

        private static int _score;

        #region Getters & Setters

        public void SetState(GameStates state) { _state = state; }

        public static void AddScore(int score) {
            _score += score;
            OnScoreChange?.Invoke();
        }

        public static int GetScore() {
            return _score;
        }

        #endregion

        #region Methods

        public bool GamePaused() { return _state != GameStates.Playing; }

        public void EndGame() {
            // TODO - End the game
            _state = GameStates.Paused;
        }

        #endregion
    }
}
