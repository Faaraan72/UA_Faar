using UnityEngine;
using UnityEngine.UI;
using System;

namespace UAFaar.Cards
{
    public class CardView : MonoBehaviour
    {
        public event Action<CardView> OnSelected;

        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Button button;

        public CardData Data { get; private set; }

        private bool isFaceUp;
        private bool isMatched;

        //Initialize Card Data
        public void Initialize(CardData data)
        {
            Data = data;
            frontImage.sprite = data.FrontSprite;
            SetFaceDown();
        }

        private void Awake()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            Debug.Log("Card Clicked");
            if (isFaceUp || isMatched)
                return;

            OnSelected?.Invoke(this);
        }
        #region Flipping
        //Flipping Logic
        public void FlipUp()
        {
            isFaceUp = true;
            frontImage.gameObject.SetActive(true);
            backImage.gameObject.SetActive(false);
        }

        public void FlipDown()
        {
            isFaceUp = false;
            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);
        }
        #endregion

        #region Match
        //Set Cards as Matched
        public void MarkAsMatched()
        {
            isMatched = true;
            button.interactable = false;
        }
        #endregion
        
        // Turn cards Back
        private void SetFaceDown()
        {
            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);
        }
    }
}
