using System.Collections;
using UnityEngine;
using QRTools.Functions;
using UnityEditor;

namespace QRTools.Audio
{
    public class PlaySound : MonoBehaviour
    {
        #region Properties & Variables
        [Tooltip("sound")]
        public Sound sound;

        [Tooltip("Reference's source")]
        public AudioSource Source;

        [Tooltip("Simple curve to change value with some functions")]
        public AnimationCurve curve;
        #endregion

        #region Runtime Methods
        private void Start()
        {
            Init();
        }
        #endregion

        #region Public Methods       
        public void Play() => Source.Play();

        public void PlaySoundOneShot(AudioClip clip) => Source.PlayOneShot(clip, 1f);

        public void PlaySoundOneShot(AudioClip clip, float volumeScale) => Source.PlayOneShot(clip, volumeScale);

        public void PlaySoundDelayed(float delay) => Source.PlayDelayed(delay);

        public float SetVolume(float value) => Source.volume = value;

        public float SetVolume(AudioSource source, float value) => source.volume = value;

        public float SetPitch(float value) => Source.pitch = value;

        public float SetStereoPan(float value) => Source.panStereo = value;

        /// <summary>
        /// Set value with a lerp
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
        /// Change sound with a lerp fade
        /// </summary>
        /// <param name="newClip"></param>
        /// <param name="newVolume"></param>
        /// <param name="curve"></param>
        /// <param name="duration"></param>
        public void ChangeSound(AudioClip newClip, float newVolume, AnimationCurve curve, float duration = 5f)
        {
            if (newClip == null || curve == null) Debug.Log(string.Format("Change sound in {0}, need to set a curve or a new clip ! ", gameObject.name));

            StartCoroutine(IEChangeSound(newClip, newVolume, curve, duration));
        }
        #endregion

        #region Private Methods
        void Init()
        {
            AudioSource s = null;
            sound.InitPlaySound(gameObject, out s);
            Source = s;
        }
        #endregion

        #region IEnumerator
        IEnumerator IEChangeSound(AudioClip newClip, float newVolume, AnimationCurve curve, float duration = 5f)
        {
            StartCoroutine(MathfExtensions.LerpValueWithAnimationCurve(
                Source.volume,
                0f,
                curve,
                (x) => SetVolume(Source, x),
                duration
                ));

            AudioSource s = sound.target.gameObject.AddComponent<AudioSource>();
            s.clip = newClip;
            s.volume = 0;
            s.pitch = Source.pitch;
            s.panStereo = Source.panStereo;
            s.Play();

            StartCoroutine(MathfExtensions.LerpValueWithAnimationCurve(
                s.volume,
                newVolume,
                curve,
                (x) => SetVolume(s, x),
                duration
                ));

            yield return new WaitForSeconds(duration);

            Destroy(Source);
            Source = s;

            yield break;
        }
        #endregion        
    }
}
