using UnityEngine;
using UnityEditor;

namespace QRTools.Functions
{
    [CustomPropertyDrawer(typeof(ListWithoutReplacement<>))]
    public class ListWithoutReplacementDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            EditorList.Show(property.FindPropertyRelative("list"));
            GUI.enabled = false;
            EditorList.Show(property.FindPropertyRelative("savedList"));
            GUI.enabled = true;

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
