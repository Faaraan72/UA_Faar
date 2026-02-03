using UnityEngine;
using TMPro;
using UAFaar.Managers;
namespace UAFaar.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject GameplayScreen;
        [SerializeField] private GameObject endScreen;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI ResultScoreText;
        [SerializeField] private TextMeshProUGUI ResultText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI timeText;
        [SerializeField] private TextMeshProUGUI movesText;
        [SerializeField] private TextMeshProUGUI currLeveltext;

        [Header("Stats Text")]
        [SerializeField] private TextMeshProUGUI bestScoreText;
        [SerializeField] private TextMeshProUGUI bestLevelText;

        [Header("EndScreen Btns")]
        [SerializeField] private GameObject RetryBtn;
        [SerializeField] private GameObject NextLevelBtn;
        



        public void ShowStart()
        {
            startScreen.SetActive(true);
            GameplayScreen.SetActive(false);
            endScreen.SetActive(false);
        }

        public void ShowGame()
        {
            startScreen.SetActive(false);
            GameplayScreen.SetActive(true);
            endScreen.SetActive(false);
        }

        public void UpdateTime(float time)
        {
            timeText.text = "Time: "+Mathf.CeilToInt(time).ToString();
        }

        public void UpdateScore(int score)
        {
            scoreText.text = $"Score: {score}";
        }
        public void UpdateMoves(int moves)
        {
            movesText.text = $"Moves: {moves}";
        }
        public void UpdateLevelText(int level)
        {
            currLeveltext.text = $"Current Level: {level}";
        }

        public void ShowEndScreen(bool won, int score)
        {
            endScreen.SetActive(true);
            if (won)
            {
                ResultText.text = "LEVEL WON";
                RetryBtn.SetActive(false);
                NextLevelBtn.SetActive(true);
            }
            else
            {
                ResultText.text = "GAME LOST";
                RetryBtn.SetActive(true);
                NextLevelBtn.SetActive(false);
            }
            
            ResultScoreText.text = $"Your Score : {score} ";
        }
        public void ShowCareerStats(int highScore, int bestLevel)
        {
            bestScoreText.text = $"Your Best Score: {highScore}";
            bestLevelText.text = $"Your Best Level: {bestLevel}";
        }
        public void Retry()
        {
            GameManager.Instance.RetryGame();
            UpdateScore(0);
        }
    }
}
