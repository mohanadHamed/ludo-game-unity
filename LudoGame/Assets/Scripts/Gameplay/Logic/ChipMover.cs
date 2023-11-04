using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI.Tiles;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Logic
{
    public static class ChipMover
    {
        private const float MoveStepDelaySeconds = 0.3f;

        public static readonly Tuple<int, int> StartTileGridPositionInTileGrid = new(1, 2);

        public static readonly Tuple<int, int> TopLeftGridPositionInTileGrid = new(0, 0);

        public static readonly Tuple<int, int> BottomRightGridPositionInTileGrid = new(5, 2);

        public static readonly Tuple<int,int> BottomLeftGridPositionInTileGrid = new(5, 0);

        public static readonly Tuple<int, int> BottomMiddleGridPositionInTileGrid = new(5, 1);

        public static readonly Tuple<int, int> TopMiddleGridPositionInTileGrid = new(0, 1);

        public static IEnumerator Move(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            if (playerChip.IsHomeReached) yield break;

            if (DistanceToHome(playerChip, ludoBoardUiComponent) < GameplayManager.Instance.LastDiceRoll) yield break;

            for (var i = 1; i <= GameplayManager.Instance.LastDiceRoll; i++)
            {
                MoveOneStep(playerChip, ludoBoardUiComponent);

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
                    .GridTiles[playerChip.CurrentGridPosition.Item1, playerChip.CurrentGridPosition.Item2].transform);
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

            var newGridPosition = new Tuple<int, int>(playerChip.CurrentGridPosition.Item1, playerChip.CurrentGridPosition.Item2);

            if (playerChip.CurrentTilesGrid == homeTileGrid &&
                playerChip.CurrentGridPosition.Equals(BottomMiddleGridPositionInTileGrid))
            {
                playerChip.IsHomeReached = true;
            }
            else if (playerChip.CurrentTilesGrid == homeTileGrid &&
                playerChip.CurrentGridPosition.Item2 == 1)
            {
                newGridPosition = Tuple.Create(playerChip.CurrentGridPosition.Item1 + 1, playerChip.CurrentGridPosition.Item2);
            }
            else if (playerChip.CurrentGridPosition.Item1 > 0 && playerChip.CurrentGridPosition.Item2 == 0)
            {
                newGridPosition = Tuple.Create(playerChip.CurrentGridPosition.Item1 - 1, playerChip.CurrentGridPosition.Item2);
            }
            else if (playerChip.CurrentGridPosition.Item1 == 0 && playerChip.CurrentGridPosition.Item2 < playerChip.CurrentTilesGrid.GridTiles.GetLength(1) - 1)
            {
                newGridPosition = Tuple.Create(playerChip.CurrentGridPosition.Item1, playerChip.CurrentGridPosition.Item2 + 1);
            }
            else if (playerChip.CurrentGridPosition.Equals(BottomRightGridPositionInTileGrid))
            {
                playerChip.CurrentTilesGrid = clockwiseNextTilesGridMap[playerChip.CurrentTilesGrid];
                newGridPosition = BottomLeftGridPositionInTileGrid;
            }
            else
            {
                newGridPosition = Tuple.Create(playerChip.CurrentGridPosition.Item1 + 1, playerChip.CurrentGridPosition.Item2);
            }

            playerChip.CurrentGridPosition = newGridPosition;
        }

        private static int DistanceToHome(PlayerChip playerChip, LudoBoardUiComponent ludoBoardUiComponent)
        {
            var homeTileGrid = ludoBoardUiComponent.GetStartingTilesGridForColorOption(playerChip.PlayerColorOption);

            if (playerChip.CurrentTilesGrid == homeTileGrid &&
                playerChip.CurrentGridPosition.Item2 == 1)
            {
                return BottomLeftGridPositionInTileGrid.Item1 - playerChip.CurrentGridPosition.Item1 + 1;
            }

            return int.MaxValue;;
        }
    }
}
