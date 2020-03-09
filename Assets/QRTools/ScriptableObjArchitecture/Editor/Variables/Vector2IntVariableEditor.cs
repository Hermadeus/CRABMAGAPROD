using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(Vector2IntVariable))]
    public class Vector2IntVariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.vector2IntValue = Vector2Int.zero;
            oldValue.vector2IntValue = Vector2Int.zero;
            initialValue.vector2IntValue = Vector2Int.zero;

            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}
