using UnityEngine;
using UAFaar.Cards;
using UAFaar.Managers;
using System.Collections;

namespace UAFaar.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private CardView firstSelected;
        private CardView secondSelected;
        
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private AudioManager audioManager;

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

        //Register Cards on itinitalization
        public void RegisterCard(CardView card)
        {
            card.OnSelected += HandleCardSelected;
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
