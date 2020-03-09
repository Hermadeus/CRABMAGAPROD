using System.Collections;
using UnityEngine;

namespace QRTools.Utilities.Tween
{
    [CreateAssetMenu(menuName = "QRTools/Utilities/Tween/TweenSequence <Float>")]
    public class TweenSequenceFloat : TweenSequence<float>
    {
        public TweenValues[] tweenValues = default;

        public IEnumerator LerpValueWithAnimationCurve(float fromValue, System.Action<float> operation)
        {
            float value = 0f;

            for (int i = 0; i < tweenValues.Length; i++)
            {
                float elapsedTime = 0.0f;
                while (elapsedTime < tweenValues[i].duration)
                {
                    yield return new WaitForEndOfFrame();
                    elapsedTime += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsedTime / tweenValues[i].duration);
                    float tt = tweenValues[i].curve.Evaluate(t);
                    value = Mathf.Lerp(fromValue, tweenValues[i].toValue, tt);
                    operation(value);
                }
                yield return new WaitForEndOfFrame();
                fromValue = value;
            }
        }

        [System.Serializable]
        public class TweenValues
        {
            public float toValue = default;
            public AnimationCurve curve = default;
            public float duration = 5f;
        }
    }
}
