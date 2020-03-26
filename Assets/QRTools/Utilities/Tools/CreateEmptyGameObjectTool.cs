using UnityEditor;
using UnityEngine;

namespace QRTools
{
    public class CreateEmptyGameObjectTool : MonoBehaviour
    {
#if UNITY_EDITOR
        [MenuItem("GameObject/Empty GameObjects With Graphics 3D", false, 0)]
        static void CreateGameObjectWith3DGraphics()
        {
            GameObject go = new GameObject();
            go.name = "Nom De Object";
            go.transform.position = Vector3.zero;
            Selection.activeObject = go;

            GameObject gr = new GameObject();
            gr.transform.position = Vector3.zero;
            gr.transform.parent = go.transform;
            gr.name = "Graphics";
            gr.AddComponent<MeshFilter>();
            gr.AddComponent<MeshRenderer>();
        }
#endif
    }
}