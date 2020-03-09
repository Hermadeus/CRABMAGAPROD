using UnityEditor;
using QRTools.Functions.Editor;

namespace QRTools.Actions
{
    [CustomEditor(typeof(ActionHook_Trigger2D))]
    public class OnTrigger2DDrawer : Editor
    {
        ActionHook_Trigger2D myTarget;

        SerializedProperty
            description,
            isActive,
            testWithTag,
            tag,
            collider,
            uniqueCollision,
            ontriggerEnterAction,
            ontriggerExitAction;

        private void OnEnable()
        {
            myTarget = (ActionHook_Trigger2D)target;
            Init();
        }

        void Init()
        {
            description = serializedObject.FindProperty("description");
            isActive = serializedObject.FindProperty("isActive");
            testWithTag = serializedObject.FindProperty("testWithTag");
            tag = serializedObject.FindProperty("tag");
            collider = serializedObject.FindProperty("targetCollider");
            uniqueCollision = serializedObject.FindProperty("uniqueCollision");
            ontriggerEnterAction = serializedObject.FindProperty("onTriggerEnter_Actions");
            ontriggerExitAction = serializedObject.FindProperty("onTriggerExit_Actions");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(description);
            EditorGUILayout.PropertyField(isActive);
            if (isActive.boolValue == true)
            {
                EditorGUILayout.PropertyField(testWithTag);

                if (testWithTag.boolValue == true)
                    EditorGUILayout.PropertyField(tag);
                else
                    EditorGUILayout.PropertyField(collider);

                EditorGUILayout.PropertyField(uniqueCollision);

                EditorGUILayout.Space();

                EditorList.Show(ontriggerEnterAction, EditorListOption.All);
                EditorList.Show(ontriggerExitAction, EditorListOption.All);

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
