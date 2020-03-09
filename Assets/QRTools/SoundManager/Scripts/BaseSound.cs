using UnityEngine;
using UnityEngine.Audio;

namespace QRTools.Audio
{
    public class BaseSound : ScriptableObject
    {
        #region Properties & Variables
        [Tooltip("Name of the sound")]
        public string soundName;

        [Tooltip("Details... Where the sound must be play, which effects to add...")]
        [TextArea(3, 5)] [SerializeField] private string detail;

        [Tooltip("Clip of the sound")]
        [SerializeField] private AudioClip clip = default;
        public AudioClip Clip
        {
            get => clip;
            set
            {
                clip = value;
            }
        }

        [Tooltip("Output of the sound")]
        [SerializeField] private AudioMixerGroup output = default;
        public AudioMixerGroup Output
        {
            get => output;
            set
            {
                output = value;
            }
        }

        [Tooltip("Volume of the sound")]
        [Range(0f,1f)]
        [SerializeField] private float volume = 1f;
        public float Volume
        {
            get => volume;
            set
            {
                volume = value;
                Source.volume = value;
                if (Volume < 0) Volume = 0; else if (Volume > 1) Volume = 1;
            }
        }

        [Tooltip("Pitch of the sound, -3 => deep tone, 3 => high pitched sound")]
        [Range(-3f, 3f)]
        [SerializeField] private float pitch = 1f;
        public float Pitch
        {
            get => pitch;
            set
            {
                pitch = value;
                source.pitch = value;
                if (Pitch < -3f) Pitch = -3f; else if (Pitch > 3f) Pitch = 3f;
            }
        }

        [Tooltip("Stereo pan of the sound, -1 => left, 1 => right")]
        [Range(-1f, 1f)]
        [SerializeField] private float stereoPan = 0f;
        public float StereoPan
        {
            get => stereoPan;
            set
            {
                stereoPan = value;
                source.panStereo = value;
                if (StereoPan < -1f) StereoPan = -1f; else if (StereoPan > 1f) StereoPan = 1f;
            }
        }

        [Range(0, 256)]
        [SerializeField] private int priority = 128;
        public int Priority
        {
            get => priority;
            set
            {
                priority = value;
                source.priority = value;
                if (Priority < 0) Priority = 0; else if (Priority > 256) Priority = 256;
            }
        }

        [Tooltip("Mute the sound")]
        [SerializeField] private bool mute = false;
        public bool Mute
        {
            get => mute;
            set
            {
                mute = value;
                source.mute = value;
            }
        }

        [Tooltip("Play on awake the sound")]
        [SerializeField] private bool playOnAwake = false;
        public bool PlayOnAwake
        {
            get => playOnAwake;
            set
            {
                playOnAwake = value;
                source.playOnAwake = value;
            }
        }

        [Tooltip("Loop the sound")]
        [SerializeField] private bool loop = false;
        public bool Loop
        {
            get => loop;
            set
            {
                loop = value;
                source.loop = value;
            }
        }

        [Tooltip("Ignore any applied effects on the audio source")]
        [SerializeField] private bool bypassEffects = false;
        public bool BypassEffects
        {
            get => bypassEffects;
            set
            {
                bypassEffects = value;
                source.bypassEffects = value;
            }
        }

        [Tooltip("Ignore any applied effects from listener")]
        [SerializeField] private bool bypassListenerEffects = false;
        public bool BypassListenerEffects
        {
            get => bypassListenerEffects;
            set
            {
                bypassListenerEffects = value;
                source.bypassListenerEffects = value;
            }
        }

        [Tooltip("Ignore any reverb zones")]
        [SerializeField] private bool bypassReverbZones = false;
        public bool BypassReverbZones
        {
            get => bypassReverbZones;
            set
            {
                bypassReverbZones = value;
                source.bypassReverbZones = value;
            }
        }

        private AudioSource source = default;
        public AudioSource Source
        {
            get => source;
            set
            {
                source = value;
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Initialize the source
        /// </summary>
        public void InitSource()
        {
            if (source == null) return;

            source.clip = Clip;
            source.volume = Volume;
            source.pitch = Pitch;
            source.panStereo = StereoPan;
            source.outputAudioMixerGroup = Output;
            source.loop = Loop;
            source.playOnAwake = PlayOnAwake;
            source.mute = Mute;
            source.bypassEffects = BypassEffects;
            source.bypassListenerEffects = BypassListenerEffects;
            source.bypassReverbZones = bypassReverbZones;
        }

        /// <summary>
        /// Initialize a source
        /// </summary>
        /// <param name="_source"></param>
        public void InitSource(AudioSource _source)
        {
            if (_source == null) return;

            _source.clip = Clip;
            _source.volume = Volume;
            _source.pitch = Pitch;
            _source.panStereo = StereoPan;
            _source.outputAudioMixerGroup = Output;
            _source.loop = Loop;
            _source.playOnAwake = PlayOnAwake;
            _source.mute = Mute;
            _source.bypassEffects = BypassEffects;
            _source.bypassListenerEffects = BypassListenerEffects;
            _source.bypassReverbZones = bypassReverbZones;
        }

        public float SetVolume(float _volume) => Volume = _volume;

        public float SetPitch(float _pitch) => Pitch = _pitch;

        public float SetStereoPan(float _panning) => stereoPan = _panning;

        public void PlaySound() => Source.Play();

        public void PlaySoundOneShot() => Source.PlayOneShot(Clip, 1f);

        public void PlaySoundOneShot(float volumeScale) => Source.PlayOneShot(Clip, volumeScale);

        public void PlaySoundDelayed(float delay) => Source.PlayDelayed(delay);

        #endregion

#if UNITY_EDITOR
        public void OnValidate()
        {
            InitSource();
        }
#endif
    }
}
