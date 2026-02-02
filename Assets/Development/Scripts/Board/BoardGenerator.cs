using UnityEngine;
using System.Collections.Generic;
using UAFaar.Cards;
using UAFaar.Core;

namespace UAFaar.Board
{
    public class BoardGenerator : MonoBehaviour
    {
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private Transform cardParent;

        [SerializeField] private GridScaler gridScaler;
        private readonly List<CardView> activeCards = new();

        //Prepares the Board
        public List<CardView> CreateBoard(List<CardData> cards , int cardCount)
        {
            ClearBoard();
            gridScaler.Configure(cardCount);
            foreach (var data in cards)
            {
                var card = Instantiate(cardPrefab, cardParent);
                card.Initialize(data);
                activeCards.Add(card);
            }

            return activeCards;
        }
        public bool AreAllCardsMatched()
        {
            foreach (var card in activeCards)
            {
                if (!card.isMatched)
                    return false;
            }
            return true;
        }

        private void ClearBoard()
        {
            foreach (var card in activeCards)
                Destroy(card.gameObject);

            activeCards.Clear();
        }
    }
}
