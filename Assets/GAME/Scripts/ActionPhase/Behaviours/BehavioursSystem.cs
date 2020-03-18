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
        NULL_MOVEMENT,
        GO_ALL_RIGHT_MOVEMENT,
        JOIN_CASTLE_MOVEMENT
    }

    public enum DetectionBehaviourEnum
    {
        CLOSEST_DETECTION
    }

    public enum DetectionRangeEnum
    {
        SHORT,
        MEDIUM,
        LONG
    }
}