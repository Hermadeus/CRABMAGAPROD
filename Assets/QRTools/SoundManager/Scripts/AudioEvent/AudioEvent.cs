///
///From https://www.youtube.com/watch?v=VBA1QCoEAX4
///

using UnityEngine;
using UnityEditor;

using Sirenix.OdinInspector;

namespace QRTools.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public string Name = default;

        public abstract void Play(AudioSource source);

        private AudioSource _previewer;

        protected virtual void OnEnable()
        {
#if UNITY_EDITOR
            _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
#endif
        }

        protected virtual void OnDisable()
        {
            DestroyImmediate(_previewer.gameObject);
        }

        [Button("Preview")]
        void TestSound()
        {
            Play(_previewer);
        }
    }
}
