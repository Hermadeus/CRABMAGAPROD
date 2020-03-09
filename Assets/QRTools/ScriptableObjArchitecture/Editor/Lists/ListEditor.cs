using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    public class ListEditor : Editor
    {
        #region SerializeProperty
        SerializedProperty description;
        SerializedProperty list;

        SerializedProperty onAdded;
        SerializedProperty onRemoved;
        SerializedProperty onCleared;

        SerializedProperty resetValue;
        SerializedProperty saveList;
        #endregion
        
        bool inGame = false;

        private void OnEnable()
        {
            InitSerializedProperty();
            EditorApplication.playModeStateChanged += ButtonChange;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawProperties();

            serializedObject.ApplyModifiedProperties();
        }

        void InitSerializedProperty()
        {
            description = serializedObject.FindProperty("description");
            list = serializedObject.FindProperty("_list");

            onAdded = serializedObject.FindProperty("onAdded");
            onRemoved = serializedObject.FindProperty("onRemoved");
            onCleared = serializedObject.FindProperty("onCleared");

            resetValue = serializedObject.FindProperty("resetValue");
            saveList = serializedObject.FindProperty("saveList");
        }

        void DrawProperties()
        {
            EditorGUILayout.PropertyField(description);

            //GUILayout.BeginVertical("HelpBox");            

            EditorList.Show(list, EditorListOption.All);

           // GUILayout.EndVertical();

            GameEventButton.DrawGameEventProperty(onAdded, inGame);
            GameEventButton.DrawGameEventProperty(onRemoved, inGame);
            GameEventButton.DrawGameEventProperty(onCleared, inGame);

            EditorGUILayout.PropertyField(resetValue);
            EditorGUILayout.PropertyField(saveList);

            ResetAll();
        }

        public void ButtonChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                inGame = true;

            if (state == PlayModeStateChange.ExitingPlayMode)
                inGame = false;
        }

        public void ResetAll()
        {
            if (GUILayout.Button("Reset", EditorStyles.miniButtonMid))
            {
                for (int i = 0; i < list.arraySize; i++)
                {
                    list.DeleteArrayElementAtIndex(i);
                }

                list.arraySize = 0;

                onAdded.objectReferenceValue = null;
                onRemoved.objectReferenceValue = null;
                onCleared.objectReferenceValue = null;
            }
        }
    }
}
