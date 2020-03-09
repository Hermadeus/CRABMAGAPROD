using UnityEngine;
using System;

namespace QRTools
{
    /// <summary>
    /// Script from DGTools -> Poulpinou
    /// </summary>
    public class TypeConstraintAttribute : PropertyAttribute
    {
        public Type type { get; private set; }

        public TypeConstraintAttribute(Type _type)
        {
            this.type = _type;
        }
    }
}
