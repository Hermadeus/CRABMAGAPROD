using UnityEditor;
using UnityEngine;

namespace QRTools.Debugging
{
    public class MenuItemAddDebugElement : MonoBehaviour
    {
        [MenuItem("GameObject/QRTools/UI/Debug/Debug Window", false, 1)]
        public static void AddCheatCodeMenu()
        {
            Canvas c = FindObjectOfType<Canvas>();
            if(c == null)
            {
                Debug.LogError("Add a canvas before");
                return;
            }

            GameObject go = Instantiate(Resources.Load<GameObject>("CheatCodeWindow"));
            go.transform.parent = c.transform;
            RectTransform rt = go.GetComponent<RectTransform>();
            rt.localScale = Vector3.one;
            rt.localPosition = Vector3.zero;
            Selection.activeObject = go;
        }
    }
}
