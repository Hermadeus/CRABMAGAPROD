using UnityEditor;
using UnityEngine;

namespace QRTools.Audio
{
    //[CustomEditor(typeof(MultipleAudioEvent))]
    public class MultipleSoundEventEditor : Editor
    {
        SerializedProperty
            baseClip,
            audioPlayMode,
            volume,
            pitch,
            delay,
            resetValue;

        private void OnEnable()
        {
            baseClip = serializedObject.FindProperty("baseClip");
            audioPlayMode = serializedObject.FindProperty("audioPlayMode");
            volume = serializedObject.FindProperty("volume");
            pitch = serializedObject.FindProperty("pitch");
            delay = serializedObject.FindProperty("delay");
            resetValue = serializedObject.FindProperty("resetValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(baseClip);
            EditorGUILayout.PropertyField(audioPlayMode);
            EditorGUILayout.PropertyField(volume);
            EditorGUILayout.PropertyField(pitch);
            EditorGUILayout.PropertyField(delay);
            EditorGUILayout.PropertyField(resetValue);
        }
    }
}
