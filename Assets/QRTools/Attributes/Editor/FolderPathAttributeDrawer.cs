using UnityEngine;
using UnityEditor;

namespace QRTools
{
    [CustomPropertyDrawer(typeof(FolderPathAttribute))]
    public class FolderPathAttributeDrawer : PropertyDrawer
    {
        const int BUTTON_SIZE = 60;
        bool modified = false;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.String:

                    FolderPathAttribute myTarget = (FolderPathAttribute)attribute;

                    string ressourcePath = Application.dataPath + "/";
                    if (!string.IsNullOrEmpty(myTarget.folderPathRestriction)) ressourcePath += myTarget.folderPathRestriction + "/";
                    string path = property.stringValue;
                    var contentRect = EditorGUI.PrefixLabel(position, label);
                    var textRect = contentRect;
                    var buttonRect = contentRect;

                    textRect.width -= BUTTON_SIZE * (myTarget.hasClearButton ? 2 : 1);
                    buttonRect.width = BUTTON_SIZE;
                    buttonRect.x = textRect.xMax;

                    path = EditorGUI.TextField(textRect, path);
                    var select = GUI.Button(buttonRect, "Browse");
                    if (select)
                    {
                        path = EditorUtility.OpenFolderPanel("Select Folder", ressourcePath + path, "");
                        modified = true;
                    }

                    if (myTarget.hasClearButton)
                    {
                        buttonRect.x += buttonRect.width;
                        var clear = GUI.Button(buttonRect, "Clear");
                        if (clear)
                        {
                            property.stringValue = null;
                            modified = false;
                        }
                    }

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (modified)
                        {
                            if (!path.Contains(ressourcePath))
                            {
                                Debug.Log("The Folder should be located in the " + myTarget.folderPathRestriction + " folder");
                                modified = false;
                                return;
                            }
                            path = path.Replace(ressourcePath, "");
                            property.stringValue = path;
                            modified = false;
                        }
                    }

                    break;
                default:
                    EditorGUI.LabelField(position, label.text, "Use folder path with string");
                    break;
            }
        }
    }
}
