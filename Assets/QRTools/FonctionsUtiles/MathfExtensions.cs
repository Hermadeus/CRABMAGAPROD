using System.Collections;
using UnityEngine;
using System;

namespace QRTools.Functions
{
    public struct MathfExtensions
    {
        /// <summary>
        /// EXEMPLE : StartCoroutine(LerpValueWithAnimationCurve(
        ///        fromValue,
        ///        toValue,
        ///        curve,
        ///        operation: (x) => fromValue.SetValue(x)
        ///        .5f
        /// </summary>
        public static IEnumerator LerpValueWithAnimationCurve(float fromValue, float toValue, AnimationCurve animationCurve, System.Action<float> operation, float duration = 5f)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = animationCurve.Evaluate(t);
                float value = Mathf.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }

        /// <summary>
        /// EXEMPLE : StartCoroutine(LerpValueWithAnimationCurve(
        ///        fromValue,
        ///        toValue,
        ///        curve,
        ///        operation: (x) => fromValue.SetValue(x)
        ///        .5f
        /// </summary>
        public static IEnumerator LerpValueWithAnimationCurve(Vector3 fromValue, Vector3 toValue, AnimationCurve animationCurve, System.Action<Vector3> operation, float duration = 5f)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = animationCurve.Evaluate(t);
                Vector3 value = Vector3.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }

        public static IEnumerator LerpValueWithAnimationCurve(Vector2 fromValue, Vector2 toValue, AnimationCurve animationCurve, System.Action<Vector2> operation, float duration = 5f)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = animationCurve.Evaluate(t);
                Vector3 value = Vector3.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }

        public static IEnumerator LerpValueWithAnimationCurve(Quaternion fromValue, Quaternion toValue, AnimationCurve animationCurve, System.Action<Quaternion> operation, float duration = 5f)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < duration)
            {
                yield return new WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / duration);
                float tt = animationCurve.Evaluate(t);
                Quaternion value = Quaternion.Lerp(fromValue, toValue, tt);
                operation(value);
            }
        }
    }
}
