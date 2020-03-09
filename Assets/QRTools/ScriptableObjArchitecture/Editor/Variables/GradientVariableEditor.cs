using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(ColorGradingVariable))]
    public class GradientVariableEditor : VariableEditor
    {
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

        protected override void ResetAll()
        {
            /*initialValue.colorValue = Color.white;
            value.colorValue = Color.white;
            oldValue.colorValue = Color.white;*/

            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
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

            ResetAll();
        }

        public new void ButtonChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                inGame = true;

            if (state == PlayModeStateChange.ExitingPlayMode)
                inGame = false;
        }
    }
}
