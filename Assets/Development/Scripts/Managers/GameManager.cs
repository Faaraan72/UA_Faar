using UnityEngine;
using UAFaar.Cards;
using UAFaar.Board;
using UAFaar.UI;
using System.Collections;
using UAFaar.Core;

namespace UAFaar.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private CardView firstSelected;
        private CardView secondSelected;
        
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private BoardGenerator boardGen;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private int currentLevel = 4;

        private bool isResolving;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
        // Start Game
        public void StartGame()
        {
            LevelData level = levelManager.GetLevel(currentLevel);
            var cards = cardLibrary.GetRandomPairs(level.pairCount);
            boardGen.CreateBoard(cards, level.pairCount * 2);
        }

        //Register Cards on itinitalization
        public void RegisterCard(CardView card)
        {
            card.OnSelected += HandleCardSelected;
        }
        // Level Completed
        private void OnLevelCompleted()
        {
            currentLevel++;

            uiManager.ShowEndScreen(
                true,
                scoreManager.Score,
                currentLevel
            );
        }

        // Event for card Selection 
        private void HandleCardSelected(CardView card)
        {
            if (isResolving)
                return;
            audioManager.PlayFlip();
            if (firstSelected == null)
            {
                firstSelected = card;
                card.FlipUp();
                return;
            }

            if (secondSelected == null && card != firstSelected)
            {
                secondSelected = card;
                card.FlipUp();
                StartCoroutine(ResolveMatch());
            }
        }

        private IEnumerator ResolveMatch()
        {
            isResolving = true;

            yield return new WaitForSeconds(0.5f);

            bool isMatch = MatchResolver.IsMatch(firstSelected, secondSelected);

            if (isMatch)
            {
                firstSelected.MarkAsMatched();
                secondSelected.MarkAsMatched();
                scoreManager.OnMatch();
                audioManager.PlayMatch();
                uiManager.UpdateScore(scoreManager.Score);
                if (boardGen.AreAllCardsMatched())
                {
                    OnLevelCompleted();
                }
            }
            else
            {
                firstSelected.FlipDown();
                secondSelected.FlipDown();
                scoreManager.OnMismatch();
                audioManager.PlayMismatch();

            }

            firstSelected = null;
            secondSelected = null;
            isResolving = false;
        }
    }
}
