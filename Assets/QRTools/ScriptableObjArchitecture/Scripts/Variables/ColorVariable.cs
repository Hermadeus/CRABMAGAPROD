using System.Collections;
using UnityEngine;

namespace QRTools.Variables
{
    [CreateAssetMenu(fileName = "New Color", menuName = "QRTools/Variables/Color", order = 10)]
    public class ColorVariable : Variable<Color>
    {
        #region Properties & Variables
        public override Color Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                PlayEvent();
            }
        }
        #endregion

        #region Publics IEnumarator
        public IEnumerator FadeOut(float time = .05f, float speedValue = .01f)
        {
            while(Value.a > 0)
            {
                Color c = Value;
                c.a -= speedValue;
                Value = c;

                yield return new WaitForSeconds(time);
            }
        }

        public IEnumerator FadeIn(float time = .05f, float speedValue = .01f)
        {
            while (Value.a < 1)
            {
                Color c = Value;
                c.a += speedValue;
                Value = c;

                yield return new WaitForSeconds(time);
            }
        }

        public IEnumerator SetAlpha(float alpha, float time = .05f, float speedValue = .01f)
        {
            if (alpha > Value.a)
            {
                while (Value.a < alpha)
                {
                    Color c = Value;
                    c.a += speedValue;
                    Value = c;

                    yield return new WaitForSeconds(time);
                }
            }else if(alpha < Value.a)
            {
                while (Value.a > alpha)
                {
                    Color c = Value;
                    c.a -= speedValue;
                    Value = c;

                    yield return new WaitForSeconds(time);
                }
            }
        }

        public IEnumerator LerpToColor(Color color, float time = 5, float speedValue = .01f)
        {
            for (float t = 0.1f; t < time; t += speedValue)
            {
                Value = Color.Lerp(Value, color, t / time);
                yield return null;
            }
        }
        #endregion
    }
}

