using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataTypes;
using Assets.Scripts.UI.StartPositions;
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

    [SerializeField] private StartPositionsComponent _startPositionsYellow;

    [SerializeField] private StartPositionsComponent _startPositionsGreen;

    [SerializeField] private StartPositionsComponent _startPositionsBlue;

    [SerializeField] private StartPositionsComponent _startPositionsRed;

    [SerializeField] private RectTransform _rtHome;

    [SerializeField] private TilesGrid[] _tilesGridArray;

    private StartPositionsComponent[] _startPositionsArray;

    private float _boardPixelSize;

    private void Awake()
    {
        _startPositionsArray = new []
        {
            _startPositionsYellow,
            _startPositionsGreen,
            _startPositionsBlue,
            _startPositionsRed,
        };
    }

    public void FixSize(float boardPixelSize)
    {
        _boardPixelSize = boardPixelSize;
        var startPositionsPixelSize = StartPositionsGridSize * CellPixelSize;
        var homePixelSize = HomeGridSize * CellPixelSize;

        _rtLudoBoard.sizeDelta = new Vector2(boardPixelSize, boardPixelSize);

        var startPositionComponentMinRow = 0;
        var startPositionComponentMinCol = 0;
        var startPositionComponentMaxRow = 5;
        var startPositionComponentMaxCol = 5;
        _startPositionsYellow.InitializePositions(startPositionComponentMinRow, startPositionComponentMinCol, startPositionComponentMaxRow, startPositionComponentMaxCol, CellPixelSize);

        startPositionComponentMinRow = 0;
        startPositionComponentMinCol = 9;
        startPositionComponentMaxRow = 5;
        startPositionComponentMaxCol = 14;
        _startPositionsGreen.InitializePositions(startPositionComponentMinRow, startPositionComponentMinCol, startPositionComponentMaxRow, startPositionComponentMaxCol, CellPixelSize);

        startPositionComponentMinRow = 9;
        startPositionComponentMinCol = 0;
        startPositionComponentMaxRow = 14;
        startPositionComponentMaxCol = 5;
        _startPositionsBlue.InitializePositions(startPositionComponentMinRow, startPositionComponentMinCol, startPositionComponentMaxRow, startPositionComponentMaxCol, CellPixelSize);

        startPositionComponentMinRow = 9;
        startPositionComponentMinCol = 9;
        startPositionComponentMaxRow = 14;
        startPositionComponentMaxCol = 14;
        _startPositionsRed.InitializePositions(startPositionComponentMinRow, startPositionComponentMinCol, startPositionComponentMaxRow, startPositionComponentMaxCol, CellPixelSize);

        foreach (var startPos in _startPositionsArray)
        {
            startPos.GetComponent<RectTransform>().sizeDelta = new Vector2(startPositionsPixelSize, startPositionsPixelSize);
        }

        foreach (var tg in _tilesGridArray)
        {
            tg.FixSize(CellPixelSize);
        }


        _rtHome.sizeDelta = new Vector2(homePixelSize, homePixelSize);
    }

    public Vector2 GetPixelPositionForRowCol(int row, int col)
    {
        return new Vector2(col * CellPixelSize, -row * CellPixelSize);
    }

    public StartPositionsComponent GetStartPositionComponentForColorOption(PlayerColorOption playerColorOption)
    {
        var map = new Dictionary<PlayerColorOption, StartPositionsComponent>()
        {
            { PlayerColorOption.Yellow, _startPositionsYellow },
            { PlayerColorOption.Green, _startPositionsGreen },
            { PlayerColorOption.Blue, _startPositionsBlue },
            { PlayerColorOption.Red, _startPositionsRed }
        };

        return map[playerColorOption];
    }
}
