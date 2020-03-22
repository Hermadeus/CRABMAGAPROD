using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

namespace QRTools.UI
{
    [CreateAssetMenu(menuName = "QRTools/UI/UIButtonParameters")]
    public class UIButtonParameters : SerializedScriptableObject
    {
        public Dictionary<string, UITheme> UIThemeDic = new Dictionary<string, UITheme>();

        public UITheme GetTheme(string _theme)
        {
            UITheme theme = null;
            UIThemeDic.TryGetValue(_theme, out theme);

            if (theme == null)
            {
                Debug.LogWarning("Theme not found");
                return null;
            }

            return theme;
        }
            
        public void InitButton(Button _btn, string _theme)
        {
            _btn.colors = GetTheme(_theme).colorBlock;
        }
    }

    [System.Serializable]
    public class UITheme
    {
        public ColorBlock colorBlock;
    }
}