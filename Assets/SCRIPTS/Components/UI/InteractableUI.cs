using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ANT.Components.HUD {
    [ExecuteInEditMode()]
    public class InteractableUI : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI Header;
        [SerializeField] private TextMeshProUGUI Content;
        [SerializeField] private LayoutElement LayoutElement;
        [Space(10)]
        [SerializeField] private int CharacterWrapLimit;

        private void Update() {
            int headerLength = Header.text.Length;
            int contentLength = Content.text.Length;

            LayoutElement.enabled = (headerLength > CharacterWrapLimit || contentLength > CharacterWrapLimit) ? true : false;
        }

        public void SetText(string headerText, string contentText) {
            Header.text = headerText;
            Content.text = contentText;
        }
    }
}
