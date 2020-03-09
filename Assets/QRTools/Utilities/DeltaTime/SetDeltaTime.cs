using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QRTools.Variables;

namespace QRTools.Utilities
{
    public class SetDeltaTime : MonoBehaviour
    {
        public FloatVariable floatVariable = default;

        private void Update()
        {
            floatVariable.Value = Time.deltaTime;
        }
    }
}