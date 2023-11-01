using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Tiles
{
    public class TileComponent : MonoBehaviour
    {
        [SerializeField] private Image _bgImage;


        public void SetBgColor(Color color)
        {
            _bgImage.color = color;
        }
    }
}
