using UnityEditor;
using UnityEngine;

namespace QRTools.Audio
{
    public class MenuAddSoundObject : MonoBehaviour
    {
        #region UNITY_EDITOR
#if UNITY_EDITOR
        [MenuItem("GameObject/QRTools/Sounds/PlaySound Object", false, 2)]
        public static void AddPlaySoundItem()
        {
            GameObject go = new GameObject();
            go.name = "Sound";
            go.AddComponent<PlaySound>();
        }

        [MenuItem("GameObject/QRTools/Sounds/Sound Manager", false, 2)]
        public static void AddSoundManager()
        {
            GameObject go = new GameObject();
            go.name = "SoundManager";
            go.AddComponent<SoundManager>();
        }

        [MenuItem("GameObject/QRTools/Sounds/MultipleSound Object", false, 2)]
        public static void AddMultipleSound()
        {
            GameObject go = new GameObject();
            go.name = "MultipleSound";
            go.AddComponent<PlayMultipleSounds>();
        }
#endif
        #endregion
    }
}
