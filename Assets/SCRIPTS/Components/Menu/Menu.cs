using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;


namespace ANT.Components.Menu {
    public class Menu : MonoBehaviour {
        [SerializeField] private Image MenuImage;
        [SerializeField] private List<TextMeshProUGUI> TextToFade;
        [SerializeField] private float FadeTime;

        private Sequence _sequence;

        private void Start() {
            _sequence = DOTween.Sequence();
            _sequence.Pause();

            _sequence.OnComplete(() => {
                Debug.Log("TO DO - Start Game");
            });

            _sequence.Join(MenuImage.DOFade(0f, FadeTime));

            foreach (TextMeshProUGUI text in TextToFade)
                _sequence.Join(text.DOFade(0f, FadeTime));
        }

        public void OnStart() {
            _sequence.Play();
        }

        public void OnExit()
        {
            Application.Quit();
        }
    }
}
