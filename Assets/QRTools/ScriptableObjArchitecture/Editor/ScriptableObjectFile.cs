using UnityEngine;
using System.IO;
using UnityEditor;

public class ScriptableObjectFile : Editor
{
    public string myPath = "assets/";

    private void OnEnable()
    {
        GetScriptableFile();
        Debug.Log(Application.dataPath);

    }

    public void GetScriptableFile()
    {
        DirectoryInfo dir = new DirectoryInfo(myPath);
        FileInfo[] info = dir.GetFiles();
        foreach (FileInfo f in info)
        {
            Debug.Log("a");
        }

    }
}
