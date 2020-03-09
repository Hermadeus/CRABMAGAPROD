using UnityEngine;

using QRTools.Functions;

namespace QRTools.UI
{
    [CreateAssetMenu(menuName = "QRTools/UI/Events/Canvas Fader")]
    public class CanvasFaderEvent : ScriptableObject
    {
        public AnimationCurve defaultCurve = default;
        public float defaultDuration = 1f;

        public void FadeOutWithLerp(CanvasGroup canvasGroup)
        {
            MonoBehaviour m = FindObjectOfType<MonoBehaviour>();
            FadeToValue(m, canvasGroup, 0f, defaultCurve, defaultDuration);
        }

        public void FadeOut(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0f;
        }

        public void FadeInWithLerp(CanvasGroup canvasGroup)
        {
            MonoBehaviour m = FindObjectOfType<MonoBehaviour>();
            FadeToValue(m, canvasGroup, 1f, defaultCurve, defaultDuration);
        }

        public void FadeIn(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1f;
        }

        public void FadeToValue(MonoBehaviour m, CanvasGroup canvasGroup, float toValue, AnimationCurve curve, float duration = 1f)
        {
            m.StartCoroutine(MathfExtensions.LerpValueWithAnimationCurve(
                canvasGroup.alpha,
                toValue,
                curve,
                (x) => canvasGroup.alpha = x,
                duration
                ));
        }

        public void FadeToValue(CanvasGroup canvasGroup, float value)
        {
            canvasGroup.alpha = value;
        }
    }
}
