using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public abstract class IA_InstantationRules : SerializedScriptableObject
    {
        public abstract Entity Instantiation(IA_Manager manager);
    }
}