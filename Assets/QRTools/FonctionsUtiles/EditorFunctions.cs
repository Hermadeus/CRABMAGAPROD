using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QRTools.Functions.Editor
{
    public static class EditorFunctions
    {
        public static void DrawImage(Rect iconRect, float iconSize, Texture2D image)
        {
            iconRect.x = (Screen.width / 2f) - (iconSize / 1.5f);
            iconRect.y = 0;
            iconRect.width = iconSize;
            iconRect.height = iconSize;

            GUI.DrawTexture(iconRect, image);
            GUILayout.Space(100);
        }
    }
}
