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

        public FloatVariable camRot = default;

        [BoxGroup("Parameters"), SerializeField]
        float
            moveOffset = 5f,
            moveSpeed = 1f;
        [BoxGroup("Parameters"), SerializeField]
        Vector2 bornes = new Vector2();

        [SerializeField] Camera camera = default;
        [SerializeField] LayerMask mask;
        Ray ray;
        RaycastHit hit;

        private void Awake()
        {
            movingInput.onSwipeDown.AddListener(OnSwipeDown);
            movingInput.onSwipeUp.AddListener(OnSwipeUp);

            transform.eulerAngles = new Vector3(camRot.Value, 0, 0);
        }

        private void Update()
        {
            ray.origin = camera.transform.position;
            ray.direction = Vector3.forward;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
            {
                Debug.Log(hit.collider.gameObject);

                for (int i = 0; i < SagamapManager.instance.castles.Count; i++)
                    SagamapManager.instance.castles[i].Deselect();
                hit.transform.GetComponent<CastleSagamap>().Select();
            }
        }

        public void OnSwipeUp()
        {
            if ((transform.eulerAngles.x % 360) + moveOffset > bornes.y)
                return;

            camRot.Value = (transform.eulerAngles.x % 360) + moveOffset;

            DOTween.To(
                () => transform.eulerAngles,
                (x) => transform.eulerAngles = x,
                new Vector3(transform.eulerAngles.x + moveOffset, 0, 0),
                moveSpeed
                );
        }

        public void OnSwipeDown()
        {
            if ((transform.eulerAngles.x % 360) - moveOffset < bornes.x)
                return;

            camRot.Value = (transform.eulerAngles.x % 360) - moveOffset;

            DOTween.To(
                () => transform.eulerAngles,
                (x) => transform.eulerAngles = x,
                new Vector3(transform.eulerAngles.x - moveOffset, 0, 0),
                moveSpeed
                );
        }
    }
}