using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace QRTools.Analytics
{
    public class AnalyticsScriptableObject : ScriptableObject
    {
        #region Properties & Variables
        [Tooltip("All value of analytics")]
        [SerializeField] private List<float> analyticValue = new List<float>();
        public List<float> AnalyticValue
        {
            get => analyticValue;
            set
            {
                analyticValue = value;
                DrawGraph();
            }
        }

        [Tooltip("Courbe of analytics")]
        [SerializeField] private AnimationCurve graph = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0, 0));

        [Tooltip("Averrage value of analytics")]
        [SerializeField] private float averrageValue;
        public float AverrageValue
        {
            get => averrageValue;
            set
            {
                averrageValue = value;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Add an analytics to <see cref="AnalyticValue"/>
        /// </summary>
        /// <param name="value"></param>
        public void AddAnalytic(float value)
        {
            AnalyticValue.Add(value);
            DrawGraph();
            AverrageValue = SetAverrageValue();
        }

        /// <summary>
        /// Reset Animation curve of this analyticsScriptableObject
        /// </summary>
        /// <returns></returns>
        public AnimationCurve ResetAnimationCurve()
        {
            graph = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0, 0));
            return graph;
        }
        #endregion

        #region privateMethods
        /// <summary>
        /// Calculate the averrage value of all values in <see cref="AnalyticValue"/>
        /// </summary>
        /// <returns></returns>
        float SetAverrageValue()
        {
            return AnalyticValue.Average();
        }

        /// <summary>
        /// Draw the <see cref="graph"/> of all values in <see cref="AnalyticValue"/> inside an animation curve
        /// </summary>
        void DrawGraph()
        {
            for (int i = 0; i < AnalyticValue.Count; i++)
            {
                float x = (50 / AnalyticValue.Count) * i;

                Keyframe k = new Keyframe(x, AnalyticValue[i]);
                graph.AddKey(k);

                Debug.Log(x);
            }
        }
        #endregion
    }
}
