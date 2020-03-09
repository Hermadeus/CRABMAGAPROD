using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    public abstract class VariableEditor : Editor
    {
        #region SerializeProperty
        protected SerializedProperty description;
        protected SerializedProperty initialValue;
        protected SerializedProperty value;
        protected SerializedProperty oldValue;

        protected SerializedProperty onValueChanged;

        protected SerializedProperty constante;
        protected SerializedProperty resetValue;
        protected SerializedProperty resetOldValue;
        #endregion

        bool inGame = false;

        void OnEnable()
        {
            InitSerializedProperty();
            EditorApplication.playModeStateChanged += ButtonChange;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawProperties();

            if (GUILayout.Button("RESET"))
                ResetAll();

            serializedObject.ApplyModifiedProperties();
        }

        void InitSerializedProperty()
        {
            description = serializedObject.FindProperty("description");
            initialValue = serializedObject.FindProperty("initialValue");
            value = serializedObject.FindProperty("_value");
            oldValue = serializedObject.FindProperty("oldValue");

            onValueChanged = serializedObject.FindProperty("OnValueChanged");

            constante = serializedObject.FindProperty("constante");
            resetValue = serializedObject.FindProperty("resetValue");
            resetOldValue = serializedObject.FindProperty("resetOldValue");
        }

        void DrawProperties()
        {
            EditorGUILayout.PropertyField(description);
            EditorGUILayout.PropertyField(initialValue);
            EditorGUILayout.PropertyField(value);
            EditorGUILayout.PropertyField(oldValue);

            GameEventButton.DrawGameEventProperty(onValueChanged, inGame);

            EditorGUILayout.PropertyField(constante);
            EditorGUILayout.PropertyField(resetValue);
            EditorGUILayout.PropertyField(resetOldValue);
        }

        public void ButtonChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                inGame = true;

            if (state == PlayModeStateChange.ExitingPlayMode)
                inGame = false;
        }

        protected abstract void ResetAll();
    }
}
