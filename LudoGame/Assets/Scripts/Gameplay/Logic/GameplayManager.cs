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
            DiceRoller.Roll(diceAnimate);
        }

        public void ResetPosition()
        {

        }
    }
}
