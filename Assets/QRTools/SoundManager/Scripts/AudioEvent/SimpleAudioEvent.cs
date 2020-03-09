using UnityEngine;
using Random = UnityEngine.Random;

using Sirenix.OdinInspector;

namespace QRTools.Audio
{
    [CreateAssetMenu(menuName = "QRTools/Audio/AudioEvents/Simple")]
    public class SimpleAudioEvent : AudioEvent
    {
        public AudioClip[] clips;

        [EnumPaging, BoxGroup("Settings")]
        public AudioPlayMode audioPlayMode = AudioPlayMode.PLAY;

        [MinMaxSlider(0, 1, true), BoxGroup("Settings")]
        public Vector2 volume = new Vector2(1,1);

        [MinMaxSlider(-3, 3, true), BoxGroup("Settings")]
        public Vector2 pitch = new Vector2(1,1);
        
        [MinMaxSlider(0,60, true), ShowIf("audioPlayMode", AudioPlayMode.PLAY_WITH_DELAY), BoxGroup("Settings")]
        public Vector2 delay = new Vector2(0, 60);

        public override void Play(AudioSource source)
        {
            if (clips.Length == 0) return;

            source.clip = clips[Random.Range(0, clips.Length)];
            source.volume = Random.Range(volume.x, volume.y);
            source.pitch = Random.Range(pitch.x, pitch.y);

            switch (audioPlayMode)
            {
                case AudioPlayMode.PLAY:
                    source.Play();
                    break;
                case AudioPlayMode.PLAY_ONE_SHOT:
                    AudioClip clip = clips[Random.Range(0, clips.Length)];
                    source.PlayOneShot(clip, Random.Range(volume.x, volume.y));
                    break;
                case AudioPlayMode.PLAY_WITH_DELAY:
                    source.PlayDelayed(Random.Range(delay.x, delay.y));
                    break;
            }
        }

        [Button("Reset")]
        void Reset()
        {
            clips = null;
            audioPlayMode = AudioPlayMode.PLAY;
            volume.x = 1;
            volume.y = 1;
            pitch.x = 1;
            pitch.y = 1;
            delay.x = 0;
            delay.y = 1;
        }
    }
    public enum AudioPlayMode
    {
        PLAY,
        PLAY_ONE_SHOT,
        PLAY_WITH_DELAY
    }
}
