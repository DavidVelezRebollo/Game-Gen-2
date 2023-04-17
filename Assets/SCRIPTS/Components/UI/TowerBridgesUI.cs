using System;
using TMPro;
using UnityEngine;

namespace ANT.Components.HUD {
    public class TowerBridgesUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI RequieredText;
        [SerializeField] private TextMeshProUGUI ActionText;

        private void Start() {
            RequieredText.enabled = false;
            ActionText.enabled = false;
        }

        public void ShowText(int currentAnts, int requieredAnts) {
            RequieredText.text = $"{currentAnts} / {requieredAnts} ants";
            ActionText.enabled = currentAnts >= requieredAnts;
            RequieredText.enabled = true;
        }

        public void HideText() {
            RequieredText.enabled = false;
            ActionText.enabled = false;
        }
    }
}
