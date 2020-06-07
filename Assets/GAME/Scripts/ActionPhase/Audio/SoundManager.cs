using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

using QRTools.Audio;

namespace CrabMaga
{
    public class SoundManager : MonoBehaviour, ISavable
    {
        public static SoundManager instance;

        private void Awake()
        {
            instance = this;
        }

        public int maxSoundPlay = 5;
        public int soundPlay;
        public float resetTimer = 1f;

        public AudioMixerGroup SFXGroup, musicGroup;

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

        public void MuteSFX()
        {
            SFXGroup.audioMixer.SetFloat("Volume", -80f);
        }

        public void SFXOn()
        {
            SFXGroup.audioMixer.SetFloat("Volume", 0f);
        }

        public void MuteMusic()
        {
            musicGroup.audioMixer.SetFloat("Volume", -80f);
        }

        public void MusicOn()
        {
            musicGroup.audioMixer.SetFloat("Volume", 0f);
        }

        public void Save()
        {
            //float x;
            //SFXGroup.audioMixer.GetFloat("Volume", out x);
            //PlayerPrefs.SetFloat("SFX", x);

            //float y;
            //musicGroup.audioMixer.GetFloat("Volume", out y);
            //PlayerPrefs.SetFloat("Music", y);
        }

        public void Load()
        {
            //float x = PlayerPrefs.GetFloat("SFX");
            //SFXGroup.audioMixer.SetFloat("Volume", x);

            //float y = PlayerPrefs.GetFloat("Music");
            //musicGroup.audioMixer.SetFloat("Volume", y);
        }
    }
}