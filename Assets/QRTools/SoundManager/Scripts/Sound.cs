using UnityEngine;
using System;

namespace QRTools.Audio
{
    [Serializable]
    public class Sound
    {
        #region Properties & Variables
        [Tooltip("Sound")]
        public BaseSound sound = default;
        [Tooltip("Choose a gameObject to initialize the audio source")]
        public GameObject target = default;
        #endregion

        #region Public Methods
        public void Init()
        {
            if (sound == null) return;
            if (sound.Source != null) return;

            if (target != null)
            {                
                sound.Source = target.AddComponent<AudioSource>();
                sound.InitSource();
            }
            else
                Debug.Log(string.Format("<color=red>Fatal error :</color> The sound {0} don't have target.", sound.name));

            PlayOnAwake();
        }

        public void InitPlaySound(GameObject _target, out AudioSource source)
        {
            source = null;

            if (sound == null) return;
            if (sound.Source != null) return;

            if (target != null)
            {
                source = target.AddComponent<AudioSource>();
                sound.InitSource(source);
            }
            else
            {
                source = _target.AddComponent<AudioSource>();
                sound.InitSource(source);
                target = _target;
            }

            if (sound.PlayOnAwake) source.Play();
        }

        public void PlaySound() => sound.Source.Play();

        public void PlaySoundOneShot() => sound.Source.PlayOneShot(sound.Clip, 1f);

        public void PlaySound(float volumeScale) => sound.Source.PlayOneShot(sound.Clip, volumeScale);

        public void PlaySoundDelayed(float delay) => sound.Source.PlayDelayed(delay);
        #endregion

        #region Private Methods
        private void PlayOnAwake()
        {
            if (!sound.PlayOnAwake) return;
            else PlaySound();
        }
        #endregion
    }

    [Serializable]
    public class SFX : Sound
    {
    }

    [Serializable]
    public class Voice : Sound
    {
    }

    [Serializable]
    public class Ambiance : Sound
    {
    }

    [Serializable]
    public class Music : Sound
    {
    }
}
