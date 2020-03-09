using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(FloatVariable))]
    public class FloatVariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.floatValue = 0;
            initialValue.floatValue = 0;
            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}