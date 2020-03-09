using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(BoolVariable))]
    public class BoolVariableEditor : Editor
    {
        BoolVariable myTarget;

        #region SerializedProperties
        SerializedProperty description;
        SerializedProperty initialValue;
        SerializedProperty value;
        SerializedProperty oldValue;

        SerializedProperty onValueChanged;
        SerializedProperty onValueChangeTrue;
        SerializedProperty onValueChangeFalse;
        #endregion

        bool inGame = false;

        private void OnEnable()
        {
            myTarget = (BoolVariable)target;
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
            initialValue = serializedObject.FindProperty("initialValue");
            value = serializedObject.FindProperty("_value");
            oldValue = serializedObject.FindProperty("oldValue");

            onValueChanged = serializedObject.FindProperty("OnValueChanged");
            onValueChangeTrue = serializedObject.FindProperty("onValueChangeToTrue");
            onValueChangeFalse = serializedObject.FindProperty("onValueChangeToFalse");
        }

        void DrawProperties()
        {
            EditorGUILayout.PropertyField(description);
            EditorGUILayout.PropertyField(initialValue);
            EditorGUILayout.PropertyField(value);
            EditorGUILayout.PropertyField(oldValue);

            GameEventButton.DrawGameEventProperty(onValueChanged, inGame);
            GameEventButton.DrawGameEventProperty(onValueChangeTrue, inGame, "(ToTrue) On Value Changed");
            GameEventButton.DrawGameEventProperty(onValueChangeFalse, inGame, "(ToFalse) On Value Changed");
            
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
                value.boolValue = false;
                oldValue.boolValue = false;
                initialValue.boolValue = false;
                onValueChanged.objectReferenceValue = null;
                onValueChangeFalse.objectReferenceValue = null;
                onValueChangeTrue.objectReferenceValue = null;
            }
        }
    }
}
