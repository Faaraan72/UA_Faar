using UnityEngine;

namespace UAFaar.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        public int Score { get; private set; }
        private int combo;

        public void OnMatch()
        {
            combo++;
            Score += 10 * combo;
            Debug.Log($"Score : {Score} , Combo : {combo}");
        }

        public void OnMismatch()
        {
            combo = 0;
        }

        public void ResetScore()
        {
            Score = 0;
            combo = 0;
        }
    }
}
