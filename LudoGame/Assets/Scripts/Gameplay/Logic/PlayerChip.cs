using Assets.Scripts.DataTypes;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Logic
{
    public class PlayerChip : MonoBehaviour
    {
        public PlayerColorOption PlayerColorOption => _playerColorOption;

        public int ChipNumber => _chipNumber;

        [SerializeField] private PlayerColorOption _playerColorOption;
        [SerializeField] private int _chipNumber;

        public void FixSize(float cellPixelSize)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(cellPixelSize, cellPixelSize);
        }

        public void SetPosition(int boardRow, int boardCol, LudoBoardUiComponent ludoBoard)
        {
            var position = ludoBoard.GetPixelPositionForRowCol(boardRow, boardCol);

            GetComponent<RectTransform>().anchoredPosition = position;
        }

        public void ResetPosition(LudoBoardUiComponent ludoBoard)
        {
            var startPositionComponent = ludoBoard.GetStartPositionComponentForColorOption(_playerColorOption);

            var pos = startPositionComponent.Positions[_chipNumber - 1];

            GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }
}
