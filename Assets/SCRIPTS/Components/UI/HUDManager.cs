using ANT.Components.Core;

using TMPro;
using UnityEngine;

namespace ANT.Components.HUD {
    public class HUDManager : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI ScoreText;

        private void Start() {
            GameManager.OnScoreChange += OnScoreChange;
            ScoreText.text = "0";
        }

        #region Auxiliar Methods

        private void OnScoreChange() {
            ScoreText.text = $"{GameManager.GetScore()}";
        }

        #endregion
    }
}
