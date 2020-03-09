using UnityEngine;

using System;

namespace QRTools.Utilities.Tween
{
    public class TweenFunction<T> : ScriptableObject where T : IEquatable<T>
    {
        public T toValue = default;
        public AnimationCurve curve = default;
        public float duration = 5f;
    }
}
