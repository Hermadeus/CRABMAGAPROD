using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(Vector3Variable))]
    public class Vector3VariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.vector3Value = Vector3.zero;
            initialValue.vector3Value = Vector3.zero;

            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}
