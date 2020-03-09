using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(ColorVariable))]
    public class ColorVariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.colorValue = Color.white;
            oldValue.colorValue = Color.white;
            initialValue.colorValue = Color.white;

            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}
