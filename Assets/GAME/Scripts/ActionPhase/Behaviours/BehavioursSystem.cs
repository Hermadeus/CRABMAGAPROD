using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using QRTools;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Data/Behaviour Dictionary")]
    public class BehavioursSystem : ScriptableObjectWithDescription
    {
        [BoxGroup("Movement System")]
        public Dictionary<SpeedEnum, float> speedState = new Dictionary<SpeedEnum, float>();

        public float GetSpeed(SpeedEnum _key)
        {
            float f_speed = 0;
            speedState.TryGetValue(_key, out f_speed);
            return f_speed / 10f;
        }

        [BoxGroup("Movement System")]
        public Dictionary<AccelerationEnum, float> accelerationState = new Dictionary<AccelerationEnum, float>();

        public float GetAcceleration(AccelerationEnum _key)
        {
            float f_acceleration = 0;
            accelerationState.TryGetValue(_key, out f_acceleration);
            return f_acceleration;
        }

        [BoxGroup("Movement System")]
        public Dictionary<MovementBehaviourEnum, IMovementBehaviour> movementBehaviours = new Dictionary<MovementBehaviourEnum, IMovementBehaviour>();

        public IMovementBehaviour GetMovementBehaviour(MovementBehaviourEnum _key)
        {
            IMovementBehaviour f_movementBehaviour = null;
            movementBehaviours.TryGetValue(_key, out f_movementBehaviour);
            return f_movementBehaviour;
        }

        [BoxGroup("Detection System")]
        public Dictionary<DetectionBehaviourEnum, IDetectSomethingBehaviour> detectionBehaviours = new Dictionary<DetectionBehaviourEnum, IDetectSomethingBehaviour>();

        public IDetectSomethingBehaviour GetDetectionBehaviour(DetectionBehaviourEnum _key)
        {
            IDetectSomethingBehaviour f_detectionBehavbiour = null;
            detectionBehaviours.TryGetValue(_key, out f_detectionBehavbiour);
            return f_detectionBehavbiour;
        }

        [BoxGroup("Detection System")]
        public Dictionary<DetectionRangeEnum, float> detectionState = new Dictionary<DetectionRangeEnum, float>();

        public float GetDetectionRange(DetectionRangeEnum _key)
        {
            float f_detectionRange = 0f;
            detectionState.TryGetValue(_key, out f_detectionRange);
            return f_detectionRange;
        }

        [BoxGroup("Attack System")]
        public Dictionary<AttackEnum, IAttackBehaviour> attackState = new Dictionary<AttackEnum, IAttackBehaviour>();

        public IAttackBehaviour GetAttackState(AttackEnum _key)
        {
            IAttackBehaviour f_attackBehaviour = null;
            attackState.TryGetValue(_key, out f_attackBehaviour);
            return f_attackBehaviour;
        }

        [BoxGroup("Passif System")]
        public Dictionary<PassifBehaviourEnum, IPassifBehaviour> passifBehaviour = new Dictionary<PassifBehaviourEnum, IPassifBehaviour>();

        public IPassifBehaviour GetPassifBehaviour(PassifBehaviourEnum _key)
        {
            IPassifBehaviour f_passifBehaviour = null;
            passifBehaviour.TryGetValue(_key, out f_passifBehaviour);
            return f_passifBehaviour;
        }
    }

    public enum SpeedEnum
    {
        STATIC,
        SLOW,
        MEDIUM,
        FAST
    }

    public enum AccelerationEnum
    {
        INSTANTANEOUS,
        SLOW,
        MEDIUM,
        FAST
    }

    public enum MovementBehaviourEnum
    {
        TARGET_MOVEMENT,
        FOLLOW_TARGET_MOVEMENT,
        NULL_MOVEMENT,
        GO_ALL_RIGHT_MOVEMENT,
        JOIN_CASTLE_MOVEMENT
    }

    public enum DetectionBehaviourEnum
    {
        CLOSEST_DETECTION,
        NULL_DETECTION
    }

    public enum DetectionRangeEnum
    {
        SHORT,
        MEDIUM,
        LONG
    }

    [System.Flags]
    public enum EntityType
    {
        CRAB_UNIT = 1,
        ENEMY = 2
    }

    public enum AttackEnum
    {
        NULL_ATTACK,
        SIMPLE_ATTACK,
        COLLIDER_ATTACK
    }

    public enum PassifBehaviourEnum
    {
        NULL_PASSIF,
        STUNT_EFFECT,
        DOUBLE_EFFECTIF_EFFECT,
        BOOST_ATTACK_SPEED_ON_OTHER_EFFECT,
        LASER_ATTACK_EFFECT,
        UP_DAMAGE_EFFECT
    }

    public enum PassifEvent
    {
        NEVER,
        ON_INSTANTIATION,
        ON_DIE,
        ON_WIN,
        ON_ATTACK,
        ON_CLICK,
        ON_OTHER_UNIT_DIE
    }

}