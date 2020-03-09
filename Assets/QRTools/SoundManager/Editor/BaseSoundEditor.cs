using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QRTools.Functions.Editor;

namespace QRTools.Audio
{
    [CustomEditor(typeof(BaseSound)), CanEditMultipleObjects]
    public class BaseSoundEditor : Editor
    {
        protected BaseSound myTarget;

        protected Texture2D icon;
        protected const float ICON_SIZE = 120f;
        protected Rect iconRect;

        protected GUISkin skin;

        protected SerializedProperty
            soundName,
            detail,
            clip,
            output,
            volume,
            pitch,
            stereoPan,
            priority,

            mute,
            playOnAwake,
            loop,

            bypassEffects,
            bypassListenerEffects,
            bypassReverbZones,
            source;

        protected void InitProperties()
        {
            soundName = serializedObject.FindProperty("soundName");
            detail = serializedObject.FindProperty("detail");
            clip = serializedObject.FindProperty("clip");
            output = serializedObject.FindProperty("output");
            volume = serializedObject.FindProperty("volume");
            pitch = serializedObject.FindProperty("pitch");
            stereoPan = serializedObject.FindProperty("stereoPan");
            priority = serializedObject.FindProperty("priority");

            mute = serializedObject.FindProperty("mute");
            playOnAwake = serializedObject.FindProperty("playOnAwake");
            loop = serializedObject.FindProperty("loop");

            bypassEffects = serializedObject.FindProperty("bypassEffects");
            bypassListenerEffects = serializedObject.FindProperty("bypassListenerEffects");
            bypassReverbZones = serializedObject.FindProperty("bypassReverbZones");

        }

        protected void DrawProperties()
        {
            EditorGUILayout.PropertyField(soundName);
            EditorGUILayout.PropertyField(detail);
            EditorGUILayout.PropertyField(clip);
            EditorGUILayout.PropertyField(output);
            EditorGUILayout.PropertyField(volume);
            EditorGUILayout.PropertyField(pitch);
            EditorGUILayout.PropertyField(stereoPan);
            EditorGUILayout.PropertyField(priority);

            EditorGUILayout.PropertyField(mute);
            EditorGUILayout.PropertyField(playOnAwake);
            EditorGUILayout.PropertyField(loop);

            EditorGUILayout.PropertyField(bypassEffects);
            EditorGUILayout.PropertyField(bypassListenerEffects);
            EditorGUILayout.PropertyField(bypassReverbZones);
        }        
    }
}
