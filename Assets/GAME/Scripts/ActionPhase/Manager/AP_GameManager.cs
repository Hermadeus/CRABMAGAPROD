using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class AP_GameManager : MonoBehaviour
    {
        public LevelData levelData = default;

        [BoxGroup("AP Informations")] 
        public List<CrabFormation> crabFormationOnBattle = new List<CrabFormation>();

        private void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}