using UnityEngine;
using TMPro;
namespace UAFaar.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject endScreen;

        [SerializeField] private TextMeshProUGUI ResultScoreText;
        [SerializeField] private TextMeshProUGUI ResultText;
        [SerializeField] private TextMeshProUGUI scoreText;
        public void ShowStart()
        {
            startScreen.SetActive(true);
            hud.SetActive(false);
            endScreen.SetActive(false);
        }

        public void ShowGame()
        {
            startScreen.SetActive(false);
            hud.SetActive(true);
            endScreen.SetActive(false);
        }

        public void ShowEnd()
        {
            startScreen.SetActive(false);
            hud.SetActive(false);
            endScreen.SetActive(true);
        }

        public void UpdateScore(int score)
        {
            scoreText.text = $"Score : {score}";
        }
        public void ShowEndScreen(bool won, int score, int nextLevel)
        {
            endScreen.SetActive(true);
            ResultText.text = won ? $"GAME WON" : "GAME LOST";
            ResultScoreText.text = $"Your Score : {score} ";
        }
    }
}
