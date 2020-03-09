using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredAttributeDrawer : PropertyDrawer
    {
        RequiredAttribute attr;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            attr = (RequiredAttribute)attribute;
            
            switch (property.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    break;
                case SerializedPropertyType.Boolean:
                    break;
                case SerializedPropertyType.Float:
                    break;
                case SerializedPropertyType.String:
                    break;
                case SerializedPropertyType.Color:
                    break;
                case SerializedPropertyType.ObjectReference:
                    EditorGUI.ObjectField(position, property);
                    if(property.objectReferenceValue == null)
                        Error();
                    break;
                case SerializedPropertyType.LayerMask:
                    break;
                case SerializedPropertyType.Enum:
                    break;
                case SerializedPropertyType.Vector2:
                    break;
                case SerializedPropertyType.Vector3:
                    break;
                case SerializedPropertyType.Vector4:
                    break;
                case SerializedPropertyType.Rect:
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    break;
                case SerializedPropertyType.AnimationCurve:
                    break;
                case SerializedPropertyType.Bounds:
                    break;
                case SerializedPropertyType.Gradient:
                    break;
                case SerializedPropertyType.Quaternion:
                    break;
                case SerializedPropertyType.ExposedReference:
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    break;
                case SerializedPropertyType.Vector3Int:
                    break;
                case SerializedPropertyType.RectInt:
                    break;
                case SerializedPropertyType.BoundsInt:
                    break;
            }
        }

        public void Error()
        {
            Debug.LogWarning("WARNING ! It's missing a reference somewhere !");
            EditorGUILayout.HelpBox(attr.message.ToString(), attr.messageType);
        }
    }
}