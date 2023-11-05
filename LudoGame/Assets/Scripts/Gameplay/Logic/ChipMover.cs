using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DataTypes;
using Assets.Scripts.UI.Tiles;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Logic
{
    public static class ChipMover
    {
        private const float MoveStepDelaySeconds = 0.3f;

        public static readonly GridCoords StartTileGridPositionInTileGrid = new(1, 2);

        public static readonly GridCoords BottomRightGridPositionInTileGrid = new(5, 2);

        public static readonly GridCoords BottomLeftGridPositionInTileGrid = new(5, 0);

        public static readonly GridCoords BottomMiddleGridPositionInTileGrid = new(5, 1);

        public static IEnumerator Move(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            if (playerChip.IsHomeReached) yield break;

            if (DistanceToHome(playerChip, ludoBoardUiComponent) < GameplayManager.Instance.LastDiceRoll) yield break;

            for (var i = 1; i <= GameplayManager.Instance.LastDiceRoll; i++)
            {
                MoveOneStep(playerChip, ludoBoardUiComponent);

                GameplayManager.Instance.PlayChipMoveAudio();

                yield return new WaitForSeconds(MoveStepDelaySeconds);
            }
        }

        private static void MoveOneStep(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            UpdateChipWithNextGridTilesAndPos(playerChip, ludoBoardUiComponent);

            UpdateChipTransformParent(playerChip, ludoBoardUiComponent);
        }

        private static void UpdateChipTransformParent(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            if (playerChip.IsHomeReached)
            {
                playerChip.transform.SetParent(
                    ludoBoardUiComponent.GetHomeTransformForColorOption(playerChip.PlayerColorOption));
            }
            else
            {
                playerChip.transform.SetParent(playerChip.CurrentTilesGrid
                    .GridTiles[playerChip.CurrentGridPosition.Row, playerChip.CurrentGridPosition.Col].transform);
            }

            playerChip.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }

        private static void UpdateChipWithNextGridTilesAndPos(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            var clockwiseNextTilesGridMap = new Dictionary<TilesGrid, TilesGrid>()
            {
                { ludoBoardUiComponent.TilesGridYellow, ludoBoardUiComponent.TilesGridGreen },
                { ludoBoardUiComponent.TilesGridGreen, ludoBoardUiComponent.TilesGridRed },
                { ludoBoardUiComponent.TilesGridRed, ludoBoardUiComponent.TilesGridBlue },
                { ludoBoardUiComponent.TilesGridBlue, ludoBoardUiComponent.TilesGridYellow }
            };

            var homeTileGrid = ludoBoardUiComponent.GetStartingTilesGridForColorOption(playerChip.PlayerColorOption);

            if (playerChip.IsPositionReset)
            {
                playerChip.CurrentTilesGrid = homeTileGrid;
                    ludoBoardUiComponent.GetStartingTilesGridForColorOption(playerChip.PlayerColorOption);
                playerChip.CurrentGridPosition = StartTileGridPositionInTileGrid;

                playerChip.IsPositionReset = false;
                return;
            }

            var newGridPosition = new GridCoords(playerChip.CurrentGridPosition.Row, playerChip.CurrentGridPosition.Col);

            if (playerChip.CurrentTilesGrid == homeTileGrid &&
                playerChip.CurrentGridPosition.Equals(BottomMiddleGridPositionInTileGrid))
            {
                playerChip.IsHomeReached = true;
            }
            else if (playerChip.CurrentTilesGrid == homeTileGrid &&
                playerChip.CurrentGridPosition.Col == 1)
            {
                newGridPosition = new GridCoords(playerChip.CurrentGridPosition.Row + 1, playerChip.CurrentGridPosition.Col);
            }
            else if (playerChip.CurrentGridPosition.Row > 0 && playerChip.CurrentGridPosition.Col == 0)
            {
                newGridPosition = new GridCoords(playerChip.CurrentGridPosition.Row - 1, playerChip.CurrentGridPosition.Col);
            }
            else if (playerChip.CurrentGridPosition.Row == 0 && playerChip.CurrentGridPosition.Col < playerChip.CurrentTilesGrid.GridTiles.GetLength(1) - 1)
            {
                newGridPosition = new GridCoords(playerChip.CurrentGridPosition.Row, playerChip.CurrentGridPosition.Col + 1);
            }
            else if (playerChip.CurrentGridPosition.Equals(BottomRightGridPositionInTileGrid))
            {
                playerChip.CurrentTilesGrid = clockwiseNextTilesGridMap[playerChip.CurrentTilesGrid];
                newGridPosition = BottomLeftGridPositionInTileGrid;
            }
            else
            {
                newGridPosition = new GridCoords(playerChip.CurrentGridPosition.Row + 1, playerChip.CurrentGridPosition.Col);
            }

            playerChip.CurrentGridPosition = newGridPosition;
        }

        private static int DistanceToHome(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            var homeTileGrid = ludoBoardUiComponent.GetStartingTilesGridForColorOption(playerChip.PlayerColorOption);

            if (playerChip.CurrentTilesGrid == homeTileGrid &&
                playerChip.CurrentGridPosition.Col == 1)
            {
                return BottomLeftGridPositionInTileGrid.Row - playerChip.CurrentGridPosition.Row + 1;
            }

            return int.MaxValue;;
        }
    }
}
