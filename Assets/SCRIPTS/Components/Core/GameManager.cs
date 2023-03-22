using UnityEngine;

namespace ANT.Components.Core {
    public enum GameStates {
        Menu,
        Paused,
        Playing
    }

    public class GameManager : MonoBehaviour {
        #region Singleton

        public GameManager Instance;

        private void Awake() {
            if (Instance != null) return;
            Instance = this;
        }

        #endregion

        private GameStates _state;

        #region Getters & Setters

        public void SetState(GameStates state) { _state = state; }

        #endregion

        #region Methods

        public bool GamePaused() { return _state != GameStates.Playing; }

        #endregion
    }
}
