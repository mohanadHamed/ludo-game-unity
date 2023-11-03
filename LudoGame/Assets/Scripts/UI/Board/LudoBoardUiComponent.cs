using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI.Tiles;
using UnityEngine;

public class LudoBoardUiComponent : MonoBehaviour
{
    public const int LudoBoardGridSize = 15;

    public const int TileGridNumColumns = 3;

    public const int TileGridNumRows = 6;

    public const int StartPositionsGridSize = 6;

    public const int HomeGridSize = 3;

    public float CellPixelSize => _boardPixelSize / LudoBoardGridSize;

    [SerializeField] private RectTransform _rtLudoBoard;

    [SerializeField] private RectTransform[] _rtStartPositionsArray;

    [SerializeField] private RectTransform _rtHome;

    [SerializeField] private TilesGrid[] _tilesGridArray;

    private float _boardPixelSize;

    // Start is called before the first frame update
    public void FixSize(float boardPixelSize)
    {
        _boardPixelSize = boardPixelSize;
        var startPositionsPixelSize = StartPositionsGridSize * CellPixelSize;
        var homePixelSize = HomeGridSize * CellPixelSize;

        _rtLudoBoard.sizeDelta = new Vector2(boardPixelSize, boardPixelSize);

        foreach (var rtStartPos in _rtStartPositionsArray)
        {
            rtStartPos.sizeDelta = new Vector2(startPositionsPixelSize, startPositionsPixelSize);
        }

        foreach (var tg in _tilesGridArray)
        {
            tg.FixSize(CellPixelSize);
        }


        _rtHome.sizeDelta = new Vector2(homePixelSize, homePixelSize);
    }
}
