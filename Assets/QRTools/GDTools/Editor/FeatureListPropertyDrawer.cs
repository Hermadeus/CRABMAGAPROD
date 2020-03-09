using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.GDTools
{
    [CustomEditor(typeof(FeatureListOption))]
    public class FeatureListPropertyDrawer : Editor
    {
        FeatureListOption myTarget;

        SerializedProperty taskName;
        SerializedProperty typeObject;
        SerializedProperty memberteam;

        SerializedProperty isDone;
        SerializedProperty date;
        SerializedProperty description;

        bool hide = false;

        private void OnEnable()
        {
            myTarget = (FeatureListOption)target;

            taskName = serializedObject.FindProperty("featureName");
            typeObject = serializedObject.FindProperty("typeObject");
            memberteam = serializedObject.FindProperty("teamMember");
            isDone = serializedObject.FindProperty("isDone");
            date = serializedObject.FindProperty("date");
            description = serializedObject.FindProperty("description");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.PropertyField(new Rect(0,3,Screen.width - 150, 20), taskName);
            hide = EditorGUI.Toggle(new Rect(Screen.width / 1.35f, 3, Screen.width - 150, 20), hide);
            EditorGUILayout.EndHorizontal();
            
            

            if (!hide)
            {
                EditorGUILayout.PropertyField(typeObject);
                EditorGUILayout.PropertyField(memberteam);
                EditorGUILayout.PropertyField(date);
                EditorGUILayout.PropertyField(description);
            }else if (hide)
            {
                EditorGUILayout.LabelField("FINISH.");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
