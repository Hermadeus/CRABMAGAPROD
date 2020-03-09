using UnityEditor;

namespace QRTools.Variables {
    [CanEditMultipleObjects]
    [CustomPropertyDrawer(typeof(GameEvent))]
    public class GameEventDrawer /*: PropertyDrawer*/
    {
        /*
        Rect gameEventRect;
        Rect buttonRect;

        //public GameEvent gameEvent;

        bool inGame = false;
        float coord;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            //property.serializedObject.Update();

            EditorApplication.playModeStateChanged += ButtonChange;

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            gameEventRect = new Rect(position.x, position.y, Screen.width / coord, position.height);
            buttonRect = new Rect(position.x + gameEventRect.width, position.y, Screen.width / 8, position.height);

            SerializedProperty gameEventProp = property.FindPropertyRelative("OnValueChanged");

            Debug.Log(gameEventProp);
            gameEventProp.objectReferenceValue = (GameEvent)EditorGUI.ObjectField(gameEventRect, label, gameEventProp.objectReferenceValue, typeof(GameEvent), false);

            if (!inGame)
            {
                if (gameEventProp.objectReferenceValue == null)
                {
                    coord = 1.6f;
                    if (GUI.Button(buttonRect, "Create")) CreateEvent(property);
                }
                else coord = 1.3f;
            }
            else if (inGame  && gameEventProp.objectReferenceValue != null)
                if (GUI.Button(buttonRect, "Raise !")) (gameEventProp.objectReferenceValue as GameEvent).Raise();

            EditorGUI.indentLevel = indent;
            //property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        private void ButtonChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
                inGame = true;

            if (state == PlayModeStateChange.ExitingPlayMode)
                inGame = false;
        }

        public void CreateEvent(SerializedProperty prop)
        {
            string path = EditorUtility.SaveFilePanelInProject("", "", "","messag");

            GameEvent asset = ScriptableObject.CreateInstance<GameEvent>();

            AssetDatabase.CreateAsset(asset, path + ".asset");
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();

            //Selection.activeObject = asset;

            prop.objectReferenceValue = asset;

            if (Event.current.commandName == "UndoRedoPerformed")
            {
                Debug.Log(1);
            }
        }
        */
    }
}
