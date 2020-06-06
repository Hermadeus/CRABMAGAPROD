using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class BloomEffect : MonoBehaviour
    {
        [Range(1, 16)]
        public float iteration = 1;

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            int width = source.width / 32;
            int height = source.height / 32;
            RenderTextureFormat format = source.format;

            Debug.Log("aaa");

            RenderTexture currentDestination = RenderTexture.GetTemporary(width, height, 0, format);

            Graphics.Blit(source, currentDestination);
            RenderTexture currentSouce = currentDestination;

            for (int i = 1; i < iteration; i++)
            {
                width /= 2;
                height /= 2;

                if (height < 2)
                    break;

                currentDestination = RenderTexture.GetTemporary(width, height, 0, format);
                Graphics.Blit(currentSouce, currentDestination);
                RenderTexture.ReleaseTemporary(currentSouce);
                currentSouce = currentDestination;
            }

            Graphics.Blit(currentSouce, destination);
            RenderTexture.ReleaseTemporary(currentSouce);
        }
    }
}