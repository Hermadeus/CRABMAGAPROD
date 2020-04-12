using System.Collections;
using UnityEngine;

using QRTools.Audio;

namespace CrabMaga
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        private void Awake()
        {
            instance = this;
        }

        public int maxSoundPlay = 5;
        public int soundPlay;
        public float resetTimer = 1f;

        public void PlaySound(AudioEvent audioEvent, AudioSource source)
        {
            if (soundPlay == maxSoundPlay)
                return;

            soundPlay++;
            audioEvent?.Play(source);
            StartCoroutine(PS());
        }

        IEnumerator PS()
        {
            yield return new WaitForSeconds(resetTimer);
            soundPlay--;
            yield break;
        }
    }
}