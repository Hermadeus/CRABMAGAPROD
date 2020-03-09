using System.Collections;

using UnityEngine;

namespace QRTools.Utilities.Tween
{
    [CreateAssetMenu(menuName = "QRTools/Utilities/Tween/Tween <Float>")]
    public class TweenFloat : TweenFunction<float>
    {
        public IEnumerator LerpValueWithAnimationCurve(float fromValue, System.Action<float> operation)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = curve.Evaluate(t);
                float value = Mathf.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }
    }
}
