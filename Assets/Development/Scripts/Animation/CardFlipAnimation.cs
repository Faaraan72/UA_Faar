using UnityEngine;
using System.Collections;

namespace UAFaar.Cards
{
    public class CardFlipAnimation : MonoBehaviour
    {
        [SerializeField] private float flipDuration = 0.25f;

        public IEnumerator Flip(bool showFront)
        {
            float half = flipDuration / 2f;

            // Shrink X
            yield return ScaleX(1f, 0f, half);

            // Swap visual happens outside (CardView)

            // Expand X
            yield return ScaleX(0f, 1f, half);
        }

        private IEnumerator ScaleX(float from, float to, float duration)
        {
            float t = 0f;
            while (t < duration)
            {
                float x = Mathf.Lerp(from, to, t / duration);
                transform.localScale = new Vector3(x, 1f, 1f);
                t += Time.deltaTime;
                yield return null;
            }
            transform.localScale = new Vector3(to, 1f, 1f);
        }
    }
}
