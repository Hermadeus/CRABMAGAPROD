﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(BoundedFloatVariable))]
    public class BoundedFloatVariableEditor : Editor
    {
        BoundedFloatVariable myTarget;

        #region SerializeProperty
        SerializedProperty description;
        SerializedProperty initialValue;
        SerializedProperty value;
        SerializedProperty oldValue;

        SerializedProperty onValueChanged;

        SerializedProperty constante;
        SerializedProperty resetValue;
        SerializedProperty resetOldValue;

        SerializedProperty minValue;
        SerializedProperty onValueMinReached;

        SerializedProperty maxValue;
        SerializedProperty onValueMaxReached;
        #endregion

        bool inGame = false;

        private void OnEnable()
        {
            myTarget = (BoundedFloatVariable)target;
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

            constante = serializedObject.FindProperty("constante");
            resetValue = serializedObject.FindProperty("resetValue");
            resetOldValue = serializedObject.FindProperty("resetOldValue");

            minValue = serializedObject.FindProperty("minValue");
            onValueMinReached = serializedObject.FindProperty("onValueMinReached");

            maxValue = serializedObject.FindProperty("maxValue");
            onValueMaxReached = serializedObject.FindProperty("onValueMaxReached");
        }

        void DrawProperties()
        {
            EditorGUILayout.PropertyField(description);
            EditorGUILayout.PropertyField(initialValue);
            EditorGUILayout.PropertyField(value);
            EditorGUILayout.PropertyField(oldValue);

            GameEventButton.DrawGameEventProperty(onValueChanged, inGame);

            EditorGUILayout.PropertyField(minValue);
            GameEventButton.DrawGameEventProperty(onValueMinReached, inGame);

            EditorGUILayout.PropertyField(maxValue);
            GameEventButton.DrawGameEventProperty(onValueMaxReached, inGame);

            EditorGUILayout.PropertyField(constante);
            EditorGUILayout.PropertyField(resetValue);
            EditorGUILayout.PropertyField(resetOldValue);

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
                value.floatValue = 0;
                oldValue.floatValue = 0;
                initialValue.floatValue = 0;
                onValueChanged.objectReferenceValue = null;
                constante.boolValue = false;
                resetValue.boolValue = true;
                resetOldValue.boolValue = true;
                onValueMaxReached.objectReferenceValue = null;
                onValueMinReached.objectReferenceValue = null;
                minValue.floatValue = 0;
                maxValue.floatValue = 0;
            }
        }
    }
}
