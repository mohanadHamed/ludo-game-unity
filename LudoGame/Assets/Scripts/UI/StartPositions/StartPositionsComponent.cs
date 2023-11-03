using System.Collections.Generic;
using Assets.Scripts.DataTypes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.StartPositions
{
    public class StartPositionsComponent : MonoBehaviour
    {
        public Vector2[] Positions => _positions;

        [SerializeField] private PlayerColorOption _playerColorOption;

        [SerializeField] private Sprite _yellowSprite;

        [SerializeField] private Sprite _greenSprite;

        [SerializeField] private Sprite _blueSprite;

        [SerializeField] private Sprite _redSprite;

        private Vector2[] _positions;

        

        private void Start()
        {
            var colorSpriteMap = new Dictionary<PlayerColorOption, Sprite>()
            {
                { PlayerColorOption.Yellow, _yellowSprite },
                { PlayerColorOption.Green, _greenSprite },
                { PlayerColorOption.Blue, _blueSprite },
                { PlayerColorOption.Red, _redSprite }
            };

            GetComponent<Image>().sprite = colorSpriteMap[_playerColorOption];
        }

        public void InitializePositions(int minRow, int minCol, int maxRow, int maxCol, float cellPixelSize)
        {
        var topLeftPosition = new Vector2(minCol * cellPixelSize, -minRow * cellPixelSize);
        var bottomRightPosition = new Vector2(maxCol * cellPixelSize, -maxRow * cellPixelSize);

        var xDiff = bottomRightPosition.x - topLeftPosition.x;
        var yDiff = bottomRightPosition.y - topLeftPosition.y;

            _positions = new Vector2[4];

            _positions[0] = new Vector2(topLeftPosition.x + xDiff * 0.4f, topLeftPosition.y + yDiff * 0.4f);
            _positions[1] = new Vector2(topLeftPosition.x + xDiff * 0.8f, topLeftPosition.y + yDiff * 0.4f);
            _positions[2] = new Vector2(topLeftPosition.x + xDiff * 0.4f, topLeftPosition.y + yDiff * 0.8f);
            _positions[3] = new Vector2(topLeftPosition.x + xDiff * 0.8f, topLeftPosition.y + yDiff * 0.8f);

        }
    }
}
