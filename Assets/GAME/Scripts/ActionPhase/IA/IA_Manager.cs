using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Sirenix.OdinInspector;

using QRTools.Functions;

namespace CrabMaga
{
    public class IA_Manager : SerializedMonoBehaviour
    {
        [FoldoutGroup("References")]
        public AP_GameManager APgameManager = default;
        [FoldoutGroup("References")]
        public GuardHouseManager guardHouseManager = default;
        [FoldoutGroup("References")]
        public EntitiesDictionary entitiesDictionary = default;
        [FoldoutGroup("References")]
        public PoolingManager poolingManager = default;

        [BoxGroup("Parameters")]
        public List<IIABehaviour> behaviours = new List<IIABehaviour>();

        [BoxGroup("Events")]
        public float timerOnGameStart = 5f;
        [BoxGroup("Events")]
        public IAEvent
            onGameStart = new IAEvent(),
            onCastleReachMiddlePV = new IAEvent(),
            onCastleReachQuartPV = new IAEvent(),
            onUnitInstantiation = new IAEvent(),
            onGeneralInvoke = new IAEvent();

        private void Awake()
        {
            InitEvents();
            StartCoroutine(OnGameStartCor());
        }

        IEnumerator OnGameStartCor()
        {
            yield return new WaitForSeconds(timerOnGameStart);
            onGameStart.Invoke(this);
            yield break;
        }

        void InitEvents()
        {
            for (int i = 0; i < behaviours.Count; i++)
            {
                if (behaviours[i].CallMoment == CallMoment.ON_GAME_START)
                    onGameStart.AddListener(behaviours[i].Play);

                if (behaviours[i].CallMoment == CallMoment.ON_CASTLE_REACH_MIDLE_PV)
                    onCastleReachMiddlePV.AddListener(behaviours[i].Play);

                if (behaviours[i].CallMoment == CallMoment.ON_CASTLE_REACH_QUART_PV)
                    onCastleReachQuartPV.AddListener(behaviours[i].Play);

                if (behaviours[i].CallMoment == CallMoment.ON_UNIT_INSTANTIATION)
                    onUnitInstantiation.AddListener(behaviours[i].Play);

                if (behaviours[i].CallMoment == CallMoment.ON_GENERAL_INVOKE)
                    onGeneralInvoke.AddListener(behaviours[i].Play);
            }
        }
        
    }

    [System.Serializable]
    public class IAEvent : UnityEvent<IA_Manager> { }
}