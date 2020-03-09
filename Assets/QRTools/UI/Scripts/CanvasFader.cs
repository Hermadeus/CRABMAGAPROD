using UnityEngine.Events;
using UnityEngine;

using Sirenix.OdinInspector;

namespace QRTools.UI
{
    public class CanvasFader : MonoBehaviour
    {
        public UnityEvent FadeInEvent = new UnityEvent();
        public UnityEvent FadeOutEvent = new UnityEvent();

        [ButtonGroup]
        void FadeIn() => FadeInEvent.Invoke();

        [ButtonGroup]
        void FadeOut() => FadeOutEvent.Invoke();
    }
}
