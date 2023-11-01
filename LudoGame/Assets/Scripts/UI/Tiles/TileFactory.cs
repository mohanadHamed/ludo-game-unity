using UnityEngine;

namespace Assets.Scripts.UI.Tiles
{
    public class TileFactory
    {
        public static TileComponent CreateTile(GameObject tilePrefab, Color color, Transform parent)
        {
            var tile = Object.Instantiate(tilePrefab, parent).GetComponent<TileComponent>();

            tile.SetBgColor(color);

            return tile;
        }
    }
}
