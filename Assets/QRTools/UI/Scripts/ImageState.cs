using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;
using UnityEngine.UI;

namespace QRTools.UI
{
    public class ImageState : SerializedMonoBehaviour
    {
        public Image image = default;

        public Dictionary<string, Sprite> spritesDic = new Dictionary<string, Sprite>();

        public void UpdateImage(string key)
        {
            Sprite spr = null;

            spritesDic.TryGetValue(key, out spr);

            if (spr == null)
                throw new System.Exception("Impossible de trouver le sprite.");

            image.sprite = spr;
        }

        [Button]
        void Link()
        {
            image = GetComponent<Image>();
        }
    }
}