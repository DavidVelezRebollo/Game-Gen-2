using UnityEngine;
using ANT.Components.Core;

namespace ANT.Components.HUD {
    public class Victory : MonoBehaviour {
        [SerializeField] private GameObject VictoryPanel;

        private void OnTriggerEnter2D(Collider2D collision) {
            if(!collision.CompareTag("Game/PlayableAnt")) return;

            VictoryPanel.SetActive(true);
            GameManager.Instance.WinGame();
        }
    }
}
