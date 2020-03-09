using UnityEngine;

using DG.Tweening;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New Easing", menuName = "QRTools/Variables/Easing", order = 25)]
    public class EasingVariable : ScriptableObject
    {
        [SerializeField] Ease ease = Ease.Linear;
        public Ease Value
        {
            get
            {
                return ease;
            }
            set
            {
                ease = value;
            }
        }
    }
}
