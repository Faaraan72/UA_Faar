using UnityEngine;

namespace UAFaar.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private AudioClip flip;
        [SerializeField] private AudioClip match;
        [SerializeField] private AudioClip mismatch;

        public void PlayFlip() => source.PlayOneShot(flip);
        public void PlayMatch() => source.PlayOneShot(match);
        public void PlayMismatch() => source.PlayOneShot(mismatch);
    }
}