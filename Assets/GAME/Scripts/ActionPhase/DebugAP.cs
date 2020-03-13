using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools.Utilities;

namespace CrabMaga
{
    public class DebugAP : MonoBehaviour, IResetable
    {
        public AP_GameManager AP_GameManager = default;

        private void Awake()
        {
            ResetObject();
        }

        public void ResetObject()
        {
            AP_GameManager.levelData.ResetObject();
        }
    }
}