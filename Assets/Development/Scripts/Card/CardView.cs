using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UAFaar.Managers;

namespace UAFaar.Cards
{
    public class CardView : MonoBehaviour
    {
        public event Action<CardView> OnSelected;

        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Button button;
        [SerializeField] private CardFlipAnimation animator;
        public CardData Data { get; private set; }

        private bool isFaceUp;
        public bool isMatched { get; private set; }

        //Initialize Card Data
        public void Initialize(CardData data)
        {
            Data = data;
            frontImage.sprite = data.FrontSprite;
            SetFaceDown();
            GameManager.Instance.RegisterCard(this);
        }

        private void Awake()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            //Debug.Log("Card Clicked");
            if (isFaceUp || isMatched)
                return;

            OnSelected?.Invoke(this); // send this card to Event
        }
        #region Flipping
        //Flipping Logic
        public void FlipUp()
        {
            isFaceUp = true;
            StartCoroutine(FlipRoutine(true));
        }

        public void FlipDown()
        {
            isFaceUp = false;
            StartCoroutine(FlipRoutine(false));
        }
        private IEnumerator FlipRoutine(bool showFront)
        {
            yield return animator.Flip(showFront);

            frontImage.gameObject.SetActive(showFront);
            backImage.gameObject.SetActive(!showFront);
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
