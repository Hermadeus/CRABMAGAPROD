using System.Collections;
using UnityEngine;

namespace QRTools.Utilities.Tween
{
    [CreateAssetMenu(menuName = "QRTools/Utilities/Tween/TweenSequence <Vector3>")]
    public class TweenSequenceVector3 : TweenSequence<Vector3>
    {
        public TweenValues[] tweenValues = default;

        public IEnumerator LerpValueWithAnimationCurve(Vector3 fromValue, System.Action<Vector3> operation)
        {
            Vector3 value = Vector3.zero;

            for (int i = 0; i < tweenValues.Length; i++)
            {
                float elapsedTime = 0.0f;
                while (elapsedTime < tweenValues[i].duration)
                {
                    yield return new WaitForEndOfFrame();
                    elapsedTime += Time.deltaTime;
                    float t = Mathf.Clamp01(elapsedTime / tweenValues[i].duration);
                    float tt = tweenValues[i].curve.Evaluate(t);
                    value = Vector3.Lerp(fromValue, tweenValues[i].toValue, tt);
                    operation(value);
                }
                yield return new WaitForEndOfFrame();
                fromValue = value;
            }
        }

        [System.Serializable]
        public class TweenValues
        {
            public Vector3 toValue = default;
            public AnimationCurve curve = default;
            public float duration = 5f;
        }
    }
}
