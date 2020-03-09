using UnityEditor;

namespace QRTools.Variables
{
    [CustomEditor(typeof(TransformVariable))]
    public class TransformVariableEditor : VariableEditor
    {
        protected override void ResetAll()
        {
            value.objectReferenceValue = null;
            oldValue.objectReferenceValue = null;
            initialValue.objectReferenceValue = null;
            onValueChanged.objectReferenceValue = null;
            constante.boolValue = false;
            resetValue.boolValue = true;
            resetOldValue.boolValue = true;
        }
    }
}
