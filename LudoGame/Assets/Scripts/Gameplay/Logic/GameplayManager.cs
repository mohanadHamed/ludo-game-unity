using System;
using Assets.Scripts.DataTypes;
using Assets.Scripts.UI.Dice;
using Assets.Scripts.UI.Game;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Logic
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;

        [SerializeField] private LudoBoardUiComponent _ludoBoardUiComponent;
        [SerializeField] private GamePanelUiComponent _gamePanelUiComponent;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            ResetPositions(_gamePanelUiComponent.PlayerChips);
        }
        public void RollDice(DiceAnimate diceAnimate)
        {
            diceAnimate.Animate();

            Action<int> success = (result) =>
            {
                diceAnimate.Stop();
                diceAnimate.SetDiceResult(result);
            };

            Action<string> error = (message) =>
            {
                diceAnimate.Stop();
                Debug.LogError(message);
            };

            StartCoroutine(RandomUtility.FetchDiceRandomNumber(success, error));
        }

        public void ResetPositions(PlayerChip[] playerChips)
        {
           // playerChip.SetPosition(13, 6, _ludoBoardUiComponent);
           
           foreach (var chip in playerChips)
           {
               chip.ResetPosition(_ludoBoardUiComponent);
           }
        }
    }
}
