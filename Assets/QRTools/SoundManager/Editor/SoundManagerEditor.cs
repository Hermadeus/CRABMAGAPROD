using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Audio
{
    [CustomEditor(typeof(SoundManager))]
    public class SoundManagerEditor : Editor
    {
        SoundManager myTarget;
        GUISkin skin;

        GUIContent c_sfx = new GUIContent("SFX", "List of sfx");
        GUIContent c_ambiance = new GUIContent("Ambiance", "List of ambiance sounds");
        GUIContent c_music = new GUIContent("Music", "List of music");
        GUIContent c_voices = new GUIContent("Voices", "List of voice sounds");

        Texture2D icon;
        const float ICON_SIZE = 120f;
        const float ICON_SIZE_LITTLE = 60f;
        Rect iconRect;

        Texture2D sfx_logo;
        Texture2D ambiance_logo;
        Texture2D music_logo;
        Texture2D voice_logo;

        Rect sfx_rect;
        Rect ambiance_rect;
        Rect music_rect;
        Rect voiceRect;

        private void OnEnable()
        {
            myTarget = (SoundManager)target;
            skin = Resources.Load<GUISkin>("GUIskins/skin");

            icon = Resources.Load<Texture2D>("Textures/SoundManagerLogo");
            sfx_logo = Resources.Load<Texture2D>("Textures/logoSFX");
            ambiance_logo = Resources.Load<Texture2D>("Textures/logoAmbiance");
            music_logo = Resources.Load<Texture2D>("Textures/logoMusic");
            voice_logo = Resources.Load<Texture2D>("Textures/logoVoice");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            //ICON
            iconRect.x = (Screen.width / 2f) - (ICON_SIZE);
            iconRect.y = 0;
            iconRect.width = ICON_SIZE;
            iconRect.height = ICON_SIZE;

            GUILayout.Space(20);
            GUI.DrawTexture(iconRect, icon);
            GUILayout.Space(20);

            //TITRE
            GUILayout.Space(50);
            GUILayout.Label("SOUND MANAGER", skin.GetStyle("Header1"));
            GUILayout.Space(30);            

            //SOUS TITRE
            GUILayout.Label("SOUND TABS", skin.GetStyle("Header2"));
            GUILayout.Space(40);

            //LOGOS
            SFX_logo();
            Ambiance_logo();
            Music_logo();
            Voice_logo();

            //TAB
            myTarget.currentTab = GUILayout.Toolbar(myTarget.currentTab, new GUIContent[] { c_sfx, c_ambiance, c_music, c_voices }, skin.GetStyle("Tab"), GUI.ToolbarButtonSize.Fixed);

            switch (myTarget.currentTab)
            {
                case 0:
                    serializedObject.Update();
                    EditorList.Show(serializedObject.FindProperty("sfxs"), EditorListOption.All);
                    serializedObject.ApplyModifiedProperties();
                    break;
                case 1:
                    serializedObject.Update();
                    EditorList.Show(serializedObject.FindProperty("ambiances"), EditorListOption.All);
                    serializedObject.ApplyModifiedProperties();
                    break;
                case 2:
                    serializedObject.Update();
                    EditorList.Show(serializedObject.FindProperty("musics"), EditorListOption.All);
                    serializedObject.ApplyModifiedProperties();
                    break;
                case 3:
                    serializedObject.Update();
                    EditorList.Show(serializedObject.FindProperty("voices"), EditorListOption.All);
                    serializedObject.ApplyModifiedProperties();
                    break;
            }

            GUILayout.Space(20);

            //SOUS TITRE
            GUILayout.Label("AUDIO MIXERS", skin.GetStyle("Header2"));

            EditorList.Show(serializedObject.FindProperty("audioMixersGroup"), EditorListOption.All);

            EditorGUILayout.HelpBox(new GUIContent("Use only unique sound here ! or use specific gameObject"));
            serializedObject.ApplyModifiedProperties();
        }

        void SFX_logo()
        {
            sfx_rect.x = (Screen.width / 12f);
            sfx_rect.y = 175;
            sfx_rect.width = ICON_SIZE_LITTLE;
            sfx_rect.height = ICON_SIZE_LITTLE;

            GUI.DrawTexture(sfx_rect, sfx_logo);
        }

        void Ambiance_logo()
        {
            ambiance_rect.x = (Screen.width / 3.75f);
            ambiance_rect.y = 175;
            ambiance_rect.width = ICON_SIZE_LITTLE;
            ambiance_rect.height = ICON_SIZE_LITTLE;

            GUI.DrawTexture(ambiance_rect, ambiance_logo);
        }

        void Music_logo()
        {
            music_rect.x = (Screen.width / 2.2f);
            music_rect.y = 175;
            music_rect.width = ICON_SIZE_LITTLE;
            music_rect.height = ICON_SIZE_LITTLE;

            GUI.DrawTexture(music_rect, music_logo);
        }

        void Voice_logo()
        {
            voiceRect.x = (Screen.width / 1.6f);
            voiceRect.y = 175;
            voiceRect.width = ICON_SIZE_LITTLE;
            voiceRect.height = ICON_SIZE_LITTLE;

            GUI.DrawTexture(voiceRect, voice_logo);
        }
    }
}
