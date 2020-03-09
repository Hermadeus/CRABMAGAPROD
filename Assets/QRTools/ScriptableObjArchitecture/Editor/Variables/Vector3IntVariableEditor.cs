using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(Vector3IntVariable))]
    public class Vector3IntVariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.vector3IntValue = Vector3Int.zero;
            oldValue.vector3IntValue = Vector3Int.zero;
            initialValue.vector3IntValue = Vector3Int.zero;

            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}
