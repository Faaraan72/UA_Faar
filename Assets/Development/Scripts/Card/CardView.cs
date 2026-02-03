using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UAFaar.Managers;

namespace UAFaar.Cards
{
    public enum CardState
    {
        FaceDown,
        FaceUp,
        Matched
    }
    public class CardView : MonoBehaviour
    {
        public event Action<CardView> OnSelected;

        [SerializeField] private Image frontImage;
        [SerializeField] private Image backImage;
        [SerializeField] private Button button;
        [SerializeField] private CardFlipAnimation animator;

        public CardState State { get; private set; } = CardState.FaceDown;

        public CardData Data { get; private set; }

        //Initialize Card Data
        public void Initialize(CardData data)
        {
            Data = data;
            frontImage.sprite = data.FrontSprite;
            GameManager.Instance.RegisterCard(this);
        }

        private void Awake()
        {
            button.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            //Debug.Log("Card Clicked");
            if (State != CardState.FaceDown)
                return;

            OnSelected?.Invoke(this); // send this card to Event
        }
        #region Flipping
        //Flipping Logic
        public void FlipUp()
        {
            State = CardState.FaceUp;
            StartCoroutine(FlipRoutine(true));
        }

        public void FlipDown()
        {
            State = CardState.FaceDown;
            StartCoroutine(FlipRoutine(false));
        }
        private IEnumerator FlipRoutine(bool showFront)
        {
            yield return animator.Flip(showFront);

            frontImage.gameObject.SetActive(showFront);
            backImage.gameObject.SetActive(!showFront);
        }
        
        public void FlipUpInstant()
        {
            frontImage.gameObject.SetActive(true);
            backImage.gameObject.SetActive(false);
        }
        public void FlipDownInstant()
        {
            frontImage.gameObject.SetActive(false);
            backImage.gameObject.SetActive(true);
        }
        #endregion

        #region Match
        //Set Cards as Matched
        public void MarkAsMatched()
        {
            State = CardState.Matched;
            button.interactable = false;
            PlayMatchAnimation();
        }
        #endregion

        
        #region Matching Aniamtions
        private void PlayMatchAnimation()
        {
            StartCoroutine(DisableRoutine());
        }
        private IEnumerator DisableRoutine()
        {
            Vector3 startScale = transform.localScale;
            Vector3 endScale = Vector3.zero;

            float duration = 0.25f;
            float t = 0f;

            while (t < duration)
            {
                transform.localScale = Vector3.Lerp(startScale, endScale, t / duration);
                t += Time.deltaTime;
                yield return null;
            }

            transform.localScale = endScale;
            gameObject.SetActive(false);
        }
        #endregion
    }
}
