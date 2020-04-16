using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QRTools.Analysis
{
    public class FPS_Counter : MonoBehaviour
    {
        private TextMeshProUGUI FPSText;

        public float updateInterval = 0.5F;

        private float accum = 0; // FPS accumulated over the interval
        private int frames = 0; // Frames drawn over the interval
        private float timeleft; // Left time for current interval

        private void Awake()
        {
            FPSText = GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
            timeleft = updateInterval;
        }

        void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            ++frames;

            // Interval ended - update GUI text and start new interval
            if (timeleft <= 0.0)
            {
                // display two fractional digits (f2 format)
                float fps = accum / frames;
                string format = System.String.Format("{0:F2} FPS", fps);
                FPSText.text = format;

                if (fps < 30)
                    FPSText.color = Color.yellow;
                else
                    if (fps < 10)
                    FPSText.color = Color.red;
                else
                    FPSText.color = Color.green;
                //	DebugConsole.Log(format,level);
                timeleft = updateInterval;
                accum = 0.0F;
                frames = 0;
            }
        }
    }
}