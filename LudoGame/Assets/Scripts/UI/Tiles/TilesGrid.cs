using System.Collections.Generic;
using Assets.Scripts.DataTypes;
using UnityEngine;

namespace Assets.Scripts.UI.Tiles
{
    public class TilesGrid : MonoBehaviour
    {
        [SerializeField] private PlayerColorOption _playerColorOption;

        [SerializeField] private GameObject _tilePrefab;

        private const int GridColumns = 3;

        private const int GridRows = 6;

        private readonly Color _defaultColor = Color.white;

        private readonly TileComponent[,] _gridTiles = new TileComponent[GridRows, GridColumns];

        private readonly Dictionary<PlayerColorOption, Color> _gridColorMap = new() {
            { PlayerColorOption.Yellow, new Color(1f, 0.949f, 0f)},
            { PlayerColorOption.Green, new Color(0.133f, 0.694f, 0.298f)},
            { PlayerColorOption.Blue, new Color(0f, 0.635f, 0.91f)},
            { PlayerColorOption.Red, new Color(0.929f, 0.11f, 0.141f)},
        };

        private void Start()
        {
            var tileThemeColor = _gridColorMap[_playerColorOption];

            for (var row = 0; row < GridRows; row++)
            {
                for (var col = 0; col < GridColumns; col++)
                {
                    var usedBgColor = (row > 0 && col == 1) || (row == 1 && col == 2) ? tileThemeColor : _defaultColor;
                    var arrowVisible = row == 0 && col == 1;

                    _gridTiles[row, col] = TileFactory.CreateTile(_tilePrefab, usedBgColor, tileThemeColor,  arrowVisible, transform);
                }
            }
        }
    }
}
