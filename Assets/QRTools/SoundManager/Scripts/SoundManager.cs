using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using QRTools.Functions;

namespace QRTools.Audio
{
    public class SoundManager : MonoBehaviour
    {
        #region Properties & Variables
        public static SoundManager instance;
        
        private List<Sound> baseSounds = new List<Sound>();

        [Tooltip("List of SFXs")]
        public List<SFX> sfxs = new List<SFX>();

        [Tooltip("List of musics")]
        public List<Music> musics = new List<Music>();

        [Tooltip("List of ambiances")]
        public List<Ambiance> ambiances = new List<Ambiance>();

        [Tooltip("List of voices")]
        public List<Voice> voices = new List<Voice>();

        public List<AudioMixerGroup> audioMixersGroup = new List<AudioMixerGroup>();

        [HideInInspector] public int currentTab = 4;
        #endregion

        #region Runtime Methods
        private void Awake()
        {
            Singleton();
            InitSounds();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Find a sound with the <see cref="BaseSound.soundName"/>
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public Sound FindSound(string _name)
        {
            Sound s = null;

            for (int i = 0; i < baseSounds.Count; i++)
                if (baseSounds[i].sound.soundName == _name)
                    s = baseSounds[i];

            return s;
        }

        /// <summary>
        /// Find audioMixer with it's name
        /// </summary>
        /// <param name="_name"></param>
        /// <returns></returns>
        public AudioMixerGroup FindAudioMixer(string _name)
        {
            AudioMixerGroup _audioMixersGroup = null;

            for (int i = 0; i < audioMixersGroup.Count; i++)
                if (audioMixersGroup[i].name == _name)
                    _audioMixersGroup = audioMixersGroup[i];

            return _audioMixersGroup;
        }

        /// <summary>
        /// Put general sound in pause
        /// </summary>
        /// <param name="state"></param>
        public void PauseSound(bool state = true) => AudioListener.pause = state;

        /// <summary>
        /// Change audio mixer value
        /// </summary>
        /// <param name="mixer"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public float SetAudioMixerValue(AudioMixer mixer, string parameterName, float value)
        {
            mixer.SetFloat(parameterName, value);
            return GetAudioMixerValue(mixer, parameterName);
        }

        /// <summary>
        /// Change audio mixer value
        /// </summary>
        /// <param name="audioMixerName"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public float SetAudioMixerValue(string audioMixerName, string parameterName, float value)
        {
            FindAudioMixer(audioMixerName).audioMixer.SetFloat(parameterName, value);
            return GetAudioMixerValue(audioMixerName, parameterName);
        }

        /// <summary>
        /// get audio mixer value
        /// </summary>
        /// <param name="mixer"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public float GetAudioMixerValue(AudioMixer mixer, string parameter)
        {
            float x;
            mixer.GetFloat(parameter, out x);
            return x;
        }

        /// <summary>
        /// Get audio mixer value
        /// </summary>
        /// <param name="audioMixerName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public float GetAudioMixerValue(string audioMixerName, string parameterName)
        {
            float x;
            FindAudioMixer(audioMixerName).audioMixer.GetFloat(parameterName, out x);
            return x;
        }            

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
        #endregion

        #region Private Methods
        /// <summary>
        /// Add all sound to a general list and initialize it
        /// </summary>
        void InitSounds()
        {
            for (int i = 0; i < sfxs.Count; i++)
                baseSounds.Add(sfxs[i]);
            for (int i = 0; i < musics.Count; i++)
                baseSounds.Add(musics[i]);
            for (int i = 0; i < ambiances.Count; i++)
                baseSounds.Add(ambiances[i]);
            for (int i = 0; i < voices.Count; i++)
                baseSounds.Add(voices[i]);

            if (baseSounds.Count == 0) return;

            for (int i = 0; i < baseSounds.Count; i++)
                baseSounds[i].Init();
        }

        /// <summary>
        /// Simple singleton
        /// </summary>
        private void Singleton()
        {
            instance = this;
        }
        #endregion
    }
}
