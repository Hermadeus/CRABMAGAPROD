using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using QRTools.UI;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class FeedbackManager : SerializedMonoBehaviour
    {
        public UnityEvent onAwake = new UnityEvent();

        [BoxGroup("References")]
        public AP_GameManager gameManager = default;

        [BoxGroup("Level Text")]
        public UIElement levelText = default;
        [BoxGroup("Level Text")]
        public float timerLevelText = 2f;

        [BoxGroup("Attack Text")]
        public UIElement attackText = default;
        [BoxGroup("Attack Text")]
        public float timerAttackText = 2f;

        [BoxGroup("Camera Travelling")]
        public CameraSlider cameraSlider = default;
        [BoxGroup("Camera Travelling")]
        public float timerCameraTravalling = 4f;

        private void Awake()
        {
            onAwake?.Invoke();
            StartCoroutine(ShowLevelText());
        }

        IEnumerator ShowLevelText()
        {
            yield return new WaitForEndOfFrame();
            levelText.Show();
            yield return new WaitForSeconds(timerLevelText);
            levelText.Hide();

            StartCoroutine(CameraTravelling());

            yield break;
        }

        IEnumerator CameraTravelling()
        {
            cameraSlider.SetSlider(cameraSlider.clampedValue.x, timerCameraTravalling);
            yield return new WaitForSeconds(timerCameraTravalling);

            StartCoroutine(ShowAttackText());

            yield break;
        }

        IEnumerator ShowAttackText()
        {
            attackText.Show();
            yield return new WaitForSeconds(timerAttackText);
            attackText.Hide();

            yield break;
        }
    }
}