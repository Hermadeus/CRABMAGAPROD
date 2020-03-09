using UnityEngine;
using UnityEditor;

namespace QRTools.Analytics
{
    [CustomEditor(typeof(FloatAnalytics))]
    public class FloatAnalyticEditor : Editor
    {
        FloatAnalytics myTarget;

        SerializedProperty analyticValue, graph, averrage;

        private void OnEnable()
        {
            myTarget = (FloatAnalytics)target;

            analyticValue = serializedObject.FindProperty("analyticValue");
            graph = serializedObject.FindProperty("graph");
            averrage = serializedObject.FindProperty("averrageValue");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginVertical("HelpBox");
            EditorGUILayout.LabelField("Analytics Value");
            EditorList.Show(analyticValue, EditorListOption.Buttons);
            EditorGUILayout.PropertyField(graph);
            GUILayout.EndVertical();

            EditorGUILayout.PropertyField(averrage);

            if (GUILayout.Button("Reset"))
            {
                analyticValue.arraySize = 0;

                myTarget.ResetAnimationCurve();

                averrage.floatValue = 0;

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
