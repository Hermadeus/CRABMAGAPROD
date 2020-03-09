using System.Collections;
using UnityEngine;

namespace QRTools.Utilities.Tween
{
    [CreateAssetMenu(menuName = "QRTools/Utilities/Tween/Tween <Quaternion>")]
    public class TweenQuaternion : TweenFunction<Quaternion>
    {
        public IEnumerator LerpValueWithAnimationCurve(Quaternion fromValue, System.Action<Quaternion> operation)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = curve.Evaluate(t);
                Quaternion value = Quaternion.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }
    }
}
