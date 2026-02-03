using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UAFaar.Cards;
using UAFaar.Core;

namespace UAFaar.Board
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private RectTransform boardRoot;

        [SerializeField] private Vector2 cardSize = new Vector2(160, 200);
        [SerializeField] private Vector2 spacing = new Vector2(20, 20);

        private readonly List<CardView> activeCards = new();

        //Prepare the Board
        public List<CardView> CreateBoard(List<CardData> cards)
        {
            ClearBoard();

            CalculateGrid(cards.Count, out int rows, out int cols);

            cardSize = CalculateScaledCardSize(rows, cols);

            float startX = -(cols - 1) * (cardSize.x + spacing.x) * 0.5f;
            float startY = (rows - 1) * (cardSize.y + spacing.y) * 0.5f;

            for (int i = 0; i < cards.Count; i++)
            {
                int row = i / cols;
                int col = i % cols;

                Vector2 position = new Vector2(
                    startX + col * (cardSize.x + spacing.x),
                    startY - row * (cardSize.y + spacing.y)
                );

                var card = Instantiate(cardPrefab, boardRoot);
                RectTransform rt = card.GetComponent<RectTransform>();
                rt.anchoredPosition = position;
                rt.sizeDelta = cardSize;

                card.Initialize(cards[i]);
                activeCards.Add(card);
            }

            return activeCards;
        }

        private void CalculateGrid(int cardCount, out int rows, out int cols)
        {
            cols = Mathf.CeilToInt(Mathf.Sqrt(cardCount));
            rows = Mathf.CeilToInt((float)cardCount / cols);
        }
        public bool AreAllCardsMatched()
        {
            foreach (var card in activeCards)
            {
                if (card.State != CardState.Matched)
                    return false;
            }
            return true;
        }

        public void ClearBoard()
        {
            foreach (var card in activeCards)
            {
                if (card != null)
                    card.gameObject.SetActive(false);
            }

            activeCards.Clear();
        }

        #region Grid Sizeing
        private Vector2 CalculateScaledCardSize(int rows, int cols)
        {
            float boardWidth = boardRoot.rect.width;
            float boardHeight = boardRoot.rect.height;

            // total spacing
            float totalSpacingX = spacing.x * (cols - 1);
            float totalSpacingY = spacing.y * (rows - 1);

            // max size each card can be
            float maxCardWidth = (boardWidth - totalSpacingX) / cols;
            float maxCardHeight = (boardHeight - totalSpacingY) / rows;

            // keep aspect ratio
            float scale = Mathf.Min(
                maxCardWidth / cardSize.x,
                maxCardHeight / cardSize.y,
                1f // never scale UP
            );

            return cardSize * scale;
        }
        #endregion
    }
}
