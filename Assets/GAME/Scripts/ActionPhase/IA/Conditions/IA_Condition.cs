using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public abstract class IA_Condition : SerializedScriptableObject, IIACondition
    {
        public abstract bool Condition(IA_Manager manager);
    }

    public interface IIACondition
    {
        bool Condition(IA_Manager manager);
    }
}