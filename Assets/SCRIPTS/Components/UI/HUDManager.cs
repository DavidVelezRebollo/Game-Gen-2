using ANT.Components.Core;

using TMPro;
using UnityEngine;

namespace ANT.Components.HUD {
    public class HUDManager : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI ScoreText;
        [SerializeField] private GameObject LoseScreen;

        private void Start() {
            GameManager.OnScoreChange += OnScoreChange;
            ScoreText.text = "0";
        }

        private void Update() {
            if (!GameManager.Instance.GameEnd()) return;
            
            LoseScreen.SetActive(true);
        }

        #region Auxiliar Methods

        private void OnScoreChange() {
            ScoreText.text = $"{GameManager.GetScore()}";
        }

        #endregion
    }
}
