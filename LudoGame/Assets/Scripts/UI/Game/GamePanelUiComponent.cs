using UnityEngine;

namespace Assets.Scripts.UI.Game
{
    public class GamePanelUiComponent : MonoBehaviour
    {
        private const float MinButtonsPanelHeight = 100f;

        [SerializeField] private LudoBoardUiComponent _ludoBoardUiComponent;

        [SerializeField] private RectTransform _rtButtonsPanel;

        // Start is called before the first frame update
        void Start()
        {
            var boardPixelSize = Mathf.Min(Screen.height - MinButtonsPanelHeight, Screen.width);
            var buttonsPanelHeight = Screen.height - boardPixelSize;

            _ludoBoardUiComponent.FixSize(boardPixelSize);
            _rtButtonsPanel.sizeDelta = new Vector2(Screen.width, buttonsPanelHeight);
        }
    }
}
