using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

using System.Collections;

namespace QRTools.Actions
{
    [CreateAssetMenu(fileName = "New Timer", menuName ="QRTools/Actions/Timer")]
    public class TimerAction : Action
    {
        #region Properties & Variables
        [Tooltip("Check this one to start timer on awake")]
        public bool startOnEnable = false;
        
        [BoxGroup("Timer")]
        public uint hours = 0;
        [Range(0, 59), BoxGroup("Timer")]
        public uint minutes = 0;
        [Range(0, 59), BoxGroup("Timer")]
        public uint seconds = 0;
        [Range(0, 999), BoxGroup("Timer")]
        public uint milliseconds = 0;

        [BoxGroup("Actions")]
        public UnityEvent
            onTimerStart = default,
            onTimerBreak = default,
            onTimerEnd = default;

        public uint CurrentHours { get { return (uint)Mathf.Floor(tempsTotalRestant / 3600); } }
        public uint CurrentMinutes { get { return (uint)Mathf.Floor(tempsTotalRestant / 60) % 60; } }
        public uint CurrentSeconds { get { return (uint)Mathf.Floor(tempsTotalRestant) % 60; } }
        public uint CurrentMilliseconds { get { return (uint)((tempsTotalRestant % 1.0f) * 1000); } }
        
        [BoxGroup("Debug"), ReadOnly, SerializeField, PropertyRange(0, "Max")] float tempsTotalRestant = 0f;
        [BoxGroup("Debug"), ReadOnly, SerializeField] bool inPause = false;

        private float Max;
        #endregion

        #region Runtime Methods
        public void OnEnable()
        {
            if (startOnEnable)
                Restart();
        }

        public override void Execute()
        {
            if (inPause)
                return;

            if (tempsTotalRestant > 0.0f)
            {
                tempsTotalRestant -= Time.deltaTime;
                if (tempsTotalRestant <= 0.0f)
                {
                    tempsTotalRestant = 0.0f;

                    onTimerEnd?.Invoke();
                }
            }
        }

        public void StartTimer()
        {
            MonoBehaviour m = FindObjectOfType<MonoBehaviour>();
            m.StartCoroutine(StartTimerCor());
        }
        public IEnumerator StartTimerCor()
        {
            onTimerStart?.Invoke();
            yield return tempsTotalRestant = CalculateTempsRestant();

            yield return new WaitForSeconds(tempsTotalRestant);
            onTimerEnd?.Invoke();
            yield break;
        }
        #endregion

        #region Public Methods
        public void Restart()
        {
            tempsTotalRestant = CalculateTempsRestant();

            onTimerStart?.Invoke();
        }

        public void Break()
        {
            if (tempsTotalRestant > 0.0f)
            {
                tempsTotalRestant = 0.0f;

                onTimerBreak?.Invoke();
            }
        }

        public void Pause(bool pauseState) => inPause = pauseState;
        #endregion

        public float CalculateTempsRestant()
        {
            tempsTotalRestant = Max = hours * 3600 + minutes * 60 + seconds + milliseconds * 0.001f;
            return tempsTotalRestant;
        }
    }
}
