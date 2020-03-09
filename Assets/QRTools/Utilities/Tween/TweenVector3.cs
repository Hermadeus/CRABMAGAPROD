using System.Collections;
using UnityEngine;

namespace QRTools.Utilities.Tween
{
    [CreateAssetMenu(menuName = "QRTools/Utilities/Tween/Tween <Vector3>")]
    public class TweenVector3 : TweenFunction<Vector3>
    {
        public IEnumerator LerpValueWithAnimationCurve(Vector3 fromValue, System.Action<Vector3> operation)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = curve.Evaluate(t);
                Vector3 value = Vector3.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }
    }
}
