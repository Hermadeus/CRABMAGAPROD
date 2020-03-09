using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Audio
{
    public class SoundDrawer<T> : PropertyDrawer
    {
        Rect soundRect;
        Rect targetRect;

        SerializedProperty sound;
        SerializedProperty target;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            soundRect = new Rect(position.x, position.y, Screen.width / 7f, position.height);
            targetRect = new Rect(position.x + Screen.width / 6.5f, position.y, Screen.width / 7f, position.height);

            EditorGUI.PropertyField(soundRect, property.FindPropertyRelative("sound"), GUIContent.none);
            EditorGUI.PropertyField(targetRect, property.FindPropertyRelative("target"), GUIContent.none);

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
