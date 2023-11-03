using System;
using Assets.Scripts.UI.Dice;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Logic
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager Instance;

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

        public void ResetPosition()
        {

        }
    }
}
