using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(Vector2Variable))]
    public class Vector2VariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.vector2Value = Vector2.zero;
            oldValue.vector2Value = Vector2.zero;
            initialValue.vector2Value = Vector2.zero;

            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}
