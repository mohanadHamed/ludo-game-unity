
using Assets.Scripts.UI.Dice;

namespace Assets.Scripts.Gameplay.Logic
{
    public static class DiceRoller
    {
        public static void Roll(DiceAnimate diceAnimate)
        {
            diceAnimate.Animate();
        }
    }
}
