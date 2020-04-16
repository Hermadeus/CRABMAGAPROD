using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.Inputs;
using QRTools.Variables;

using DG.Tweening;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class SM_CameraController : MonoBehaviour
    {
        public InputTouch movingInput = default;

        public FloatVariable mapRot = default;

        public Transform map = default;

        public MapCharger mapCharger = default;

        [BoxGroup("Parameters"), SerializeField]
        float
            moveOffset = 5f,
            moveSpeed = 1f;
        [BoxGroup("Parameters"), SerializeField]
        Vector2 bornes = new Vector2();

        [SerializeField] Camera cam = default;
        [SerializeField] LayerMask mask;
        Ray ray;
        RaycastHit hit;

        [ReadOnly] public Vector3 currentRotation;

        private void Awake()
        {
            movingInput.onSwipeDown.AddListener(OnSwipeDown);
            movingInput.onSwipeUp.AddListener(OnSwipeUp);

            currentRotation = new Vector3(mapRot.Value, 0, -90);
            mapCharger.ChunkIndex = Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);
        }

        private void Update()
        {
            currentRotation = new Vector3(currentRotation.x % 360f, currentRotation.y % 360f, currentRotation.z % 360f);
            map.transform.eulerAngles = currentRotation;

            ray.origin = cam.transform.position;
            ray.direction = Vector3.forward;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                for (int i = 0; i < SagamapManager.instance.castles.Count; i++)
                    SagamapManager.instance.castles[i].Deselect();
                hit.transform.GetComponent<CastleSagamap>().Select();
            }
        }

        public void OnSwipeUp()
        {
            if (currentRotation.x + moveOffset > 15)
                return;

            mapRot.Value = currentRotation.x + moveOffset;
            mapCharger.ChunkIndex = Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);

            DOTween.To(
                () => currentRotation,
                (x) => currentRotation = x,
                new Vector3(currentRotation.x + moveOffset, (currentRotation.y), (currentRotation.z)),
                moveSpeed
                );
        }

        public void OnSwipeDown()
        {
            mapRot.Value = currentRotation.x - moveOffset;
            mapCharger.ChunkIndex =Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);

            DOTween.To(
                () => currentRotation,
                (x) => currentRotation = x,
                new Vector3(currentRotation.x - moveOffset, (currentRotation.y), (currentRotation.z)),
                moveSpeed
                );
        }
    }
}