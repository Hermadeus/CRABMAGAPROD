using UnityEngine;
using UnityEditor;
using QRTools.Functions.Editor;

namespace QRTools.Audio {
    [CustomEditor(typeof(MusicSO)), CanEditMultipleObjects]
    public class MusicSOEditor : BaseSoundEditor
    {
        private void OnEnable()
        {
            myTarget = (MusicSO)target;

            icon = Resources.Load<Texture2D>("Textures/logoMusic");

            skin = Resources.Load<GUISkin>("GUIskins/skin");

            InitProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorFunctions.DrawImage(iconRect, ICON_SIZE, icon);

            GUILayout.Label("MUSIC", skin.GetStyle("MUSIC"));

            DrawProperties();
            
            if (GUILayout.Button("RESET"))
            {
                Reset();
            }

            serializedObject.ApplyModifiedProperties();
        }

        protected void Reset()
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
