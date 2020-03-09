using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using QRTools.Functions.Editor;

namespace QRTools.Audio
{
    [CustomEditor(typeof(SfxSO)), CanEditMultipleObjects]
    public class SfxSOEditor : BaseSoundEditor
    {
        private void OnEnable()
        {
            myTarget = (SfxSO)target;

            icon = Resources.Load<Texture2D>("Textures/logoSFX");

            skin = Resources.Load<GUISkin>("GUIskins/skin");

            InitProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorFunctions.DrawImage(iconRect, ICON_SIZE, icon);

            GUILayout.Label("SFX", skin.GetStyle("SFX"));

            DrawProperties();

            if (GUILayout.Button("RESET"))
            {
                Reset();
            }

            serializedObject.ApplyModifiedProperties();
        }

        void Reset()
        {
            if (soundName == null)
                return;

            soundName.stringValue = null;
            clip.objectReferenceValue = null;
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
