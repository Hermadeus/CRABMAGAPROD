using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QRTools.Functions.Editor;

namespace QRTools.Audio
{
    [CustomEditor(typeof(MultipleSound))]
    public class MultipleSoundEditor : Editor
    {
        protected BaseSound myTarget;

        protected Texture2D icon;
        protected const float ICON_SIZE = 120f;
        protected Rect iconRect;

        protected GUISkin skin;

        protected SerializedProperty
            soundName,
            detail,
            clips,
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

        private void OnEnable()
        {
            myTarget = (MultipleSound)target;

            icon = Resources.Load<Texture2D>("Textures/logoRandom");

            skin = Resources.Load<GUISkin>("GUIskins/skin");

            InitProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorFunctions.DrawImage(iconRect, ICON_SIZE, icon);

            GUILayout.Label("MULTIPLE SOUNDS", skin.GetStyle("RANDOMSOUND"));

            DrawProperties();

            if (GUILayout.Button("RESET"))
            {
                Reset();
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected void InitProperties()
        {
            soundName = serializedObject.FindProperty("soundName");
            detail = serializedObject.FindProperty("detail");
            clips = serializedObject.FindProperty("clips");
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
            EditorList.Show(clips, EditorListOption.All);
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

        protected void Reset()
        {
            if (soundName == null)
                return;

            soundName.stringValue = null;
            clips.arraySize = 0;
            output.objectReferenceValue = null;
            volume.floatValue = 1f;
            pitch.floatValue = 1f;
            stereoPan.floatValue = 0f;
            priority.intValue = 128;

            mute.boolValue = false;
            playOnAwake.boolValue = false;
            loop.boolValue = false;

            bypassEffects.boolValue = false;
            bypassListenerEffects.boolValue = false;
            bypassReverbZones.boolValue = false;

        }
    }
}
