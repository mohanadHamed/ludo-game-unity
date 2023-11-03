using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.UI.Dice
{
    public class DiceAnimate : MonoBehaviour
    {
        public bool IsAnimating => _animateRoutine != null;

        [SerializeField] private Sprite[] _frames;

        private Coroutine _animateRoutine;

        public void Animate()
        {
            if (IsAnimating)
            {
                Stop();
            }

            _animateRoutine = StartCoroutine(AnimateInternal());
        }

        public void Stop()
        {
            if (_animateRoutine == null) return;

            StopCoroutine(_animateRoutine);
            GetComponent<RectTransform>().rotation = Quaternion.identity;
            _animateRoutine = null;
        }

        public void SetDiceResult(int result)
        {
            GetComponent<Image>().sprite = _frames[Math.Clamp(result - 1, 0, _frames.Length)];
        }

        private IEnumerator AnimateInternal()
        {
            while (true)
            {
                var rand = new System.Random((int)(Time.time * 1000));
                var frameIndex = rand.Next(0, _frames.Length);
                var rotation = new Vector3(0, 0, rand.Next(-180, 180));

                GetComponent<Image>().sprite = _frames[frameIndex];
                GetComponent<RectTransform>().Rotate(rotation);
               yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
