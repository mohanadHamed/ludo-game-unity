using System;
using Assets.Scripts.DataTypes;
using Assets.Scripts.UI.Tiles;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Logic
{
    public class PlayerChip : MonoBehaviour
    {
        public PlayerColorOption PlayerColorOption => _playerColorOption;

        public bool IsPositionReset { get; set; }

        public bool IsHomeReached { get; set; }

        public TilesGrid CurrentTilesGrid { get; set; }

        public Tuple<int,int> CurrentGridPosition { get; set; }

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

            transform.SetParent(pos.transform);
            GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            IsPositionReset = true;
            IsHomeReached = false;
            CurrentGridPosition = new Tuple<int, int>(-1, -1);
            CurrentTilesGrid = ludoBoard.GetStartingTilesGridForColorOption(PlayerColorOption);
        }

        public void ChipClick()
        {
            StartCoroutine(GameplayManager.Instance.MoveChip(this));
        }
    }
}
