using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using QRTools;

namespace CrabMaga
{
    public abstract class IA_Behaviour : ScriptableObjectWithDescription, IIABehaviour
    {
        [SerializeField] IIACondition[] conditions = default;
        public IIACondition[] Conditions { get => conditions; set => conditions = value; }

        [SerializeField] CallMoment callMoment = CallMoment.NULL; 
        public CallMoment CallMoment { get => callMoment; set => callMoment = value; }

        [SerializeField] protected IA_InstantationRules instantiationRule = default;

        public virtual void CallEvent(IA_Manager manager)
        {
        }

        public bool TestCondition(IA_Manager manager)
        {
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!conditions[i].Condition(manager))
                    return false;
            }

            CallEvent(manager);
            return true;
        }
    }

    public interface IIABehaviour
    {
        IIACondition[] Conditions { get; set; }
        CallMoment CallMoment { get; set; }

        bool TestCondition(IA_Manager manager);
        void CallEvent(IA_Manager manager);
    }

    public enum CallMoment
    {
        NULL,
        ON_GAME_START,
        EVERY_FRAME,
        ON_ENEMY_KILL,
        ON_CRAB_KILL,
        ON_LEADER_KILL,
        ON_CASTLE_REACH_MIDLE_PV,
        ON_CASTLE_REACH_QUART_PV
    }
}