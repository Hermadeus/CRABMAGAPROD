using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    public static partial class GameEventButton
    {
        public static void DrawGameEventButton(SerializedProperty property)
        {
            if (GUILayout.Button("Create", EditorStyles.miniButtonRight, GUILayout.Width(60f)))
            {
                string path = EditorUtility.SaveFilePanelInProject("", "", "", "Message");
                GameEvent asset = ScriptableObject.CreateInstance<GameEvent>();

                AssetDatabase.CreateAsset(asset, path + ".asset");
                AssetDatabase.SaveAssets();

                EditorUtility.FocusProjectWindow();

                Selection.activeObject = asset;

                property.objectReferenceValue = asset;
            }
        }

        public static void DrawGameEventButtonRaise(SerializedProperty property, GameEvent gameEvent)
        {
            gameEvent = (GameEvent)property.objectReferenceValue;

            if (GUILayout.Button("Raise", EditorStyles.miniButtonRight, GUILayout.Width(60f)))
                gameEvent.Raise();
        }

        public static void DrawGameEventProperty(SerializedProperty property, bool inGame)
        {
            if (inGame)
            {
                if (property.objectReferenceValue != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(property);
                    GameEventButton.DrawGameEventButtonRaise(property, (GameEvent)property.objectReferenceValue);
                    EditorGUILayout.EndHorizontal();
                }
                else if (property.objectReferenceValue == null)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
            else if (!inGame)
            {
                if (property.objectReferenceValue == null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(property);
                    GameEventButton.DrawGameEventButton(property);
                    EditorGUILayout.EndHorizontal();
                }
                else if (property.objectReferenceValue != null)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
        }

        public static void DrawGameEventProperty(SerializedProperty property, bool inGame, string labelName)
        {
            if (inGame)
            {
                if (property.objectReferenceValue != null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(property, new GUIContent(labelName));
                    GameEventButton.DrawGameEventButtonRaise(property, (GameEvent)property.objectReferenceValue);
                    EditorGUILayout.EndHorizontal();
                }
                else if (property.objectReferenceValue == null)
                {
                    EditorGUILayout.PropertyField(property, new GUIContent(labelName));
                }
            }
            else if (!inGame)
            {
                if (property.objectReferenceValue == null)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PropertyField(property, new GUIContent(labelName));
                    GameEventButton.DrawGameEventButton(property);
                    EditorGUILayout.EndHorizontal();
                }
                else if (property.objectReferenceValue != null)
                {
                    EditorGUILayout.PropertyField(property, new GUIContent(labelName));
                }
            }
        }
    }
}
