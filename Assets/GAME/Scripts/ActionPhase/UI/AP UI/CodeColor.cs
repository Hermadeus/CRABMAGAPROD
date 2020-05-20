using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRABMAGA/Code color")]
    public class CodeColor : SerializedScriptableObject
    {
        public Dictionary<Triforce, Color> triforceColors = new Dictionary<Triforce, Color>();

        public Color GetColor(Triforce triforce)
        {
            Color c;
            triforceColors.TryGetValue(triforce, out c);
            return c;
        }
    }
}