using UnityEditor;

namespace QRTools.Events
{
    [CustomEditor(typeof(Event_Trigger2D))]
    public class EventOntrigger2DDrawer : Editor
    {
        Event_Trigger2D myTarget;

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
            myTarget = (Event_Trigger2D)target;
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
            ontriggerEnterAction = serializedObject.FindProperty("onTriggerEnter");
            ontriggerExitAction = serializedObject.FindProperty("onTriggerExit");
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

                EditorGUILayout.PropertyField(ontriggerEnterAction);
                EditorGUILayout.PropertyField(ontriggerExitAction);

            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
