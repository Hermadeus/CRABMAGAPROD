﻿using UnityEngine;
using Sirenix.OdinInspector;

namespace QRTools
{
    public class ScriptableObjectWithDescription : SerializedScriptableObject
    {
        [TextArea(3, 5)] string description = default;
    }
}
