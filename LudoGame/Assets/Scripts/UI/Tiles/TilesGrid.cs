using UnityEngine;

namespace Assets.Scripts.UI.Tiles
{
    public class TilesGrid : MonoBehaviour
    {
        private const int GridColumns = 3;

        private const int GridRows = 6;

        private readonly Color _defaultColor = Color.white;
        
        [SerializeField] private Color _gridColor;

        [SerializeField] private GameObject _tilePrefab;

        private void Start()
        {
            for (var row = 0; row < GridRows; row++)
            {
                for (var col = 0; col < GridColumns; col++)
                {
                    var usedColor = (row > 0 && col == 1) || (row == 1 && col == 2)
                        ? _gridColor
                        : _defaultColor;

                    TileFactory.CreateTile(_tilePrefab, usedColor, transform);
                }
            }
        }
    }
}
