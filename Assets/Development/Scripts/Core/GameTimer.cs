using UnityEngine;
using System;

namespace UAFaar.Core
{
    public class GameTimer : MonoBehaviour
    {
        public event Action<float> OnTick;
        public event Action OnTimeUp;

        private float timeRemaining;
        private bool running;

        public void StartTimer(float duration)
        {
            timeRemaining = duration;
            running = true;
            OnTick?.Invoke(timeRemaining);
        }

        public void StopTimer()
        {
            running = false;
        }

        private void Update()
        {
            if (!running)
                return;

            timeRemaining -= Time.deltaTime;
            OnTick?.Invoke(timeRemaining);

            if (timeRemaining <= 0f)
            {
                running = false;
                OnTimeUp?.Invoke();
            }
        }
    }
}
