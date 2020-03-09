using System.Collections.Generic;
using UnityEngine;
using QRTools.Functions;

namespace QRTools.Audio
{
    public class PlayMultipleSounds : MonoBehaviour
    {
        #region Properties & Variables
        [Tooltip("Reference a MultipleSound scriptable object")]
        public MultipleSound multipleSounds;

        private List<AudioClip> _soundList = new List<AudioClip>();
        private List<AudioClip> _SoundList
        {
            get => _soundList;
            set
            {
                _soundList = value;                
            }
        }

        [Tooltip("Reference's source")]
        public AudioSource Source;

        [Tooltip("Simple curve to use with some functions")]
        public AnimationCurve curve = new AnimationCurve();
        #endregion

        #region Runtime Methods
        private void Awake()
        {
            SaveSoundList();
            if(Source == null) Initialize();
        }
        #endregion

        #region Public Methods
        public void PlaySound() => Source.Play();

        public void PlaySoundOneShot(AudioClip clip) => Source.PlayOneShot(clip, 1f);

        public void PlaySoundOneShot(AudioClip clip, float volumeScale) => Source.PlayOneShot(clip, volumeScale);

        public void PlaySoundDelayed(float delay) => Source.PlayDelayed(delay);

        public float SetVolume(float value) => Source.volume = value;

        public float SetPitch(float value) => Source.pitch = value;

        public float SetStereoPan(float value) => Source.panStereo = value;

        /// <summary>
        /// Set a value with a lerp
        /// </summary>
        /// <param name="fromValue"></param>
        /// <param name="toValue"></param>
        /// <param name="animationCurve"></param>
        /// <param name="operation"></param>
        /// <param name="duration"></param>
        public void SetValue(float fromValue, float toValue, AnimationCurve animationCurve, System.Action<float> operation, float duration = 5f)
        {
            StartCoroutine(MathfExtensions.LerpValueWithAnimationCurve(
                    fromValue,
                    toValue,
                    animationCurve,
                    operation,
                    duration
                    ));
        }

        /// <summary>
        /// Save all sound of <see cref="MultipleSound"/>'s reference in a private list
        /// </summary>
        public void SaveSoundList()
        {
            for (int i = 0; i < multipleSounds.Clips.Count; i++)
                _soundList.Add(multipleSounds.Clips[i]);
        }

        /// <summary>
        /// Find a soun on the <see cref="_SoundList"/>
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public AudioClip FindSound(string _name)
        {
            AudioClip clip = null;

            for (int i = 0; i < _soundList.Count; i++)
                if (_soundList[i].name == _name)
                    clip = _soundList[i];

            return clip;
        }

        /// <summary>
        /// Play a random sound of the <see cref="_SoundList"/>
        /// </summary>
        public void PlayRamndomSound()
        {
            int x = Random.Range(0, _SoundList.Count);
            Source.clip = _SoundList[x];
            PlaySound();
        }

        /// <summary>
        /// Play a random sound of the <see cref="_SoundList"/>
        /// </summary>
        /// <param name="soundOneShoot"></param>
        public void PlayRandomSound(bool soundOneShoot)
        {
            int x = Random.Range(0, _SoundList.Count);
            PlaySoundOneShot(_SoundList[x]);
        }

        /// <summary>
        /// Play a random sound of the <see cref="_SoundList"/> without replacement with a PlaySoundOneShot function
        /// </summary>
        public void PlayRandomSoundWithoutReplacement()
        {
            int x = Random.Range(0, _SoundList.Count);
            Source.clip = _SoundList[x];
            PlaySound();
            RemoveAt(x);
        }

        /// <summary>
        /// Play a random sound of the <see cref="_SoundList"/> without replacement with a PlaySoundOneShot function
        /// </summary>
        /// <param name="soundOneShoot"></param>
        public void PlayRamdomSoundWithoutReplacement(bool soundOneShoot)
        {
            int x = Random.Range(0, _SoundList.Count);
            PlaySoundOneShot(_SoundList[x]);
            RemoveAt(x);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initialize the source
        /// </summary>
        void Initialize()
        {
            if (Source == null) Source = gameObject.AddComponent<AudioSource>();
            multipleSounds.InitSource(Source);
        }

        /// <summary>
        /// Remove at and assure that the list can't be empty
        /// </summary>
        /// <param name="index"></param>
        void RemoveAt(int index)
        {
            _SoundList.RemoveAt(index);
            if (_soundList.Count == 0)
                SaveSoundList();
        }
        #endregion
    }
}
