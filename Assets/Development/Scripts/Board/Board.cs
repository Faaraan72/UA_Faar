using UnityEngine;
using System.Collections.Generic;
using UAFaar.Cards;

namespace UAFaar.Board
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private CardView cardPrefab;
        [SerializeField] private Transform cardParent;

        private readonly List<CardView> activeCards = new();

        //Prepares the Board
        public List<CardView> CreateBoard(List<CardData> cards)
        {
            ClearBoard();

            foreach (var data in cards)
            {
                var card = Instantiate(cardPrefab, cardParent);
                card.Initialize(data);
                activeCards.Add(card);
            }

            return activeCards;
        }

        private void ClearBoard()
        {
            foreach (var card in activeCards)
                Destroy(card.gameObject);

            activeCards.Clear();
        }
    }
}
