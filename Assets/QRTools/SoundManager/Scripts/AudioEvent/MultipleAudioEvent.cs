using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

using QRTools.Functions;

using Sirenix.OdinInspector;

namespace QRTools.Audio {
    [CreateAssetMenu(menuName = "QRTools/Audio/AudioEvents/Multiple")]
    public class MultipleAudioEvent : AudioEvent
    {
        public List<AudioClip> baseClip = new List<AudioClip>();

        public ListWithoutReplacement<AudioClip> audioClips;

        [EnumPaging, BoxGroup("Settings")]
        public AudioPlayMode audioPlayMode = AudioPlayMode.PLAY;

        [MinMaxSlider(0, 1, true), BoxGroup("Settings")]
        public Vector2 volume = new Vector2(1, 1);

        [MinMaxSlider(-3, 3, true), BoxGroup("Settings")]
        public Vector2 pitch = new Vector2(1, 1);

        [MinMaxSlider(0, 60, true), ShowIf("audioPlayMode", AudioPlayMode.PLAY_WITH_DELAY), BoxGroup("Settings")]
        public Vector2 delay = new Vector2(0, 0);

        [BoxGroup("Settings")]
        public bool ResetValue = true;

        protected override void OnEnable()
        {
            base.OnEnable();
            audioClips = new ListWithoutReplacement<AudioClip>(baseClip);
        }

        public override void Play(AudioSource source)
        {
            source.pitch = Random.Range(pitch.x, pitch.y);
            source.volume = Random.Range(volume.x, volume.y);
            switch (audioPlayMode)
            {
                case AudioPlayMode.PLAY:
                    source.clip = audioClips.GetElementWithoutReplacement(true);
                    source.Play();
                    break;
                case AudioPlayMode.PLAY_ONE_SHOT:
                    source.PlayOneShot(audioClips.GetElementWithoutReplacement(true));
                    break;
                case AudioPlayMode.PLAY_WITH_DELAY:
                    source.clip = audioClips.GetElementWithoutReplacement(true);
                    source.PlayDelayed(Random.Range(delay.x, delay.y));
                    break;
            }
        }

        [Button("Reset")]
        void Reset()
        {
            baseClip = null;
            audioPlayMode = AudioPlayMode.PLAY;
            volume.x = 1;
            volume.y = 1;
            pitch.x = 1;
            pitch.y = 1;
            delay.x = 0f;
            delay.y = 1f;
            ResetValue = true;
        }
    }
}
