using UnityEngine;
using UAFaar.Cards;
using UAFaar.Board;
using UAFaar.UI;
using System.Collections;
using System.Collections.Generic;
using UAFaar.Core;

namespace UAFaar.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } // Singleton Instance

        //Selected Cards
        private CardView firstSelected;
        private CardView secondSelected;
        
        [Header("Script References")]
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private CardLibrary cardLibrary;
        [SerializeField] private BoardGenerator boardGen;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameTimer gameTimer;

        //Stats
        private int currentLevel;
        private int currentScore;
        private int highestLevelReached;
        private int highScore;
        
        // Check Bool 
        private bool isResolving;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
           
            gameTimer.OnTick += uiManager.UpdateTime; // Subscribe Timer Event
            gameTimer.OnTimeUp += HandleTimeUp; // Subscribe TimeUp Event
        }
        private void Start()
        {
            LoadCareerStats();          //Load Stats
            uiManager.ShowStart();      // Show game StartScreen
        }
        private void LoadCareerStats()
        {
            SaveData data = SaveSystem.Load();          //Get ,Set prev Data

            highestLevelReached = data.highestLevelReached;
            highScore = data.highScore;

            uiManager.ShowCareerStats(highScore, highestLevelReached);
        }

        // Start Game
        public void StartGame()
        {
            LevelData level = levelManager.GetLevel(currentLevel);      //currentlevel
            var cards = cardLibrary.GetRandomPairs(level.pairCount);    //get Pars from level data
            boardGen.CreateBoard(cards, level.pairCount * 2);           // generate Board
            gameTimer.StartTimer(level.timeLimit);                      //Start Timer
        }

        //Register Cards on itinitalization
        public void RegisterCard(CardView card)
        {
            card.OnSelected += HandleCardSelected;          //Subscribe the card selection event
        }



        // Event for card Selection 
        private void HandleCardSelected(CardView card)
        {
            if (isResolving)                // if already resolving  return
                return;

            audioManager.PlayFlip();        // audio

            if (firstSelected == null)      
            {
                firstSelected = card;       // if it's 1st Add and flip
                card.FlipUp();
                return;
            }

            if (secondSelected == null && card != firstSelected)
            {
                secondSelected = card;      // if it's 2nd Add, flip and Check
                card.FlipUp();
                StartCoroutine(ResolveMatch());
            }
        }

        private IEnumerator ResolveMatch()
        {
            isResolving = true;             //checking

            yield return new WaitForSeconds(0.5f);

            bool isMatch = MatchResolver.IsMatch(firstSelected, secondSelected);            // returns matched or not based on id

            if (isMatch)
            {
                firstSelected.MarkAsMatched();                      //if matched, mark both as matched, AddScore,playSound,check if all matched
                secondSelected.MarkAsMatched();
                scoreManager.OnMatch();
                audioManager.PlayMatch();
                uiManager.UpdateScore(scoreManager.Score);
                if (boardGen.AreAllCardsMatched())
                {
                    OnLevelCompleted();                     // if all matched , level completed
                }
            }
            else
            {
                firstSelected.FlipDown();               // not matched ?  flip down, playSound ,reset Combo
                secondSelected.FlipDown();
                scoreManager.OnMismatch();
                audioManager.PlayMismatch();

            }

            firstSelected = null;                   //empty the holders
            secondSelected = null;
            isResolving = false;
        }

        // Level Completed
        private void OnLevelCompleted()
        {
            currentScore = scoreManager.Score;          //if currScore is greater than highest user Score, make it highestScore

            if (currentScore > highScore)
                highScore = currentScore;

            if (currentLevel > highestLevelReached)         // Same for LevelS
                highestLevelReached = currentLevel;

            SaveCareerStats();                              // Save Stats

            currentLevel++;                             //ncrement level

            uiManager.ShowEndScreen(true, currentScore);    // Show completed Screen

            gameTimer.StopTimer();                          //Stop timer
        }
        private void HandleTimeUp()                 //Time Up Same way as loose
        {
            gameTimer.StopTimer();

            currentScore = scoreManager.Score;

            if (currentScore > highScore)
                highScore = currentScore;

            if (currentLevel > highestLevelReached)         
                highestLevelReached = currentLevel;

            SaveCareerStats();

            uiManager.ShowEndScreen(false,scoreManager.Score);
        }

        //retry function , reset everything
        public void RetryGame()
        {
            // Stop any running resolution
            StopAllCoroutines();

            //Load Stats
            LoadCareerStats();

            // Reset selection state
            firstSelected = null;
            secondSelected = null;
            isResolving = false;

            // Reset score
            scoreManager.ResetScore();

            // Reset curr level
            currentLevel = 0;

            // Clear board
            boardGen.ClearBoard();

            // Show start screen
            uiManager.ShowStart();
        }
        
        // Save Stats
        private void SaveCareerStats()
        {
            SaveSystem.Save(new SaveData
            {
                highScore = highScore,
                highestLevelReached = highestLevelReached
            });
        }
    }
}
