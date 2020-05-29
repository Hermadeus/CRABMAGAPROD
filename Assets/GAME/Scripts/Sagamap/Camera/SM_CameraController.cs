using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using QRTools.Inputs;
using QRTools.Variables;

using DG.Tweening;

using Sirenix.OdinInspector;
using System;

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

        public Transform ciel;

        public LevelData level4;
        public float bornesSupChapter2 = 320;

        public Transform door;

        private void Awake()
        {
            movingInput.onSwipeDown.AddListener(OnSwipeDown);
            movingInput.onSwipeUp.AddListener(OnSwipeUp);

            currentRotation = new Vector3(mapRot.Value, 0, -90);
            mapCharger.ChunkIndex = Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);

            if (level4.asWin)
            {
                bornes.y = bornesSupChapter2;
                GoToNextChapter();
            }
        }

        public CastleSagamap lastCastle;

        public void GoToNextCastle()
        {
            if (lastCastle == null)
                lastCastle = SagamapManager.instance.castles[0];

            int y = 1000;

            for (int i = 0; i < SagamapManager.instance.castles.Count; i++)
            {
                if (SagamapManager.instance.castles[i] == lastCastle)
                    y = i;
            }

            y++;

            if (y > SagamapManager.instance.castles.Count)
                return;

            GoToPos(SagamapManager.instance.castles[y].posRotCamera);
        }

        public void GoToPreviousCastle()
        {
            if (lastCastle == null)
                lastCastle = SagamapManager.instance.castles[0];

            int y = -1;

            for (int i = 0; i < SagamapManager.instance.castles.Count; i++)
            {
                if (SagamapManager.instance.castles[i] == lastCastle)
                    y = i;
            }

            y--;

            if (y < 0 /*|| SagamapManager.instance.castles[y].levelData.isLock*/)
                return;

            GoToPos(SagamapManager.instance.castles[y].posRotCamera);
        }

        public void GoToPos(float rot)
        {
            DOTween.To(() => currentRotation, (x) => currentRotation = x, new Vector3(rot, currentRotation.y, currentRotation.z), 1.5f).SetEase(Ease.InOutSine).OnComplete(OpenDoorChapter);
        }

        private void GoToNextChapter()
        {
            DOTween.To(() => currentRotation, (x) => currentRotation = x, new Vector3(-120f, currentRotation.y, currentRotation.z), 1.5f).SetEase(Ease.InOutSine).OnComplete(OpenDoorChapter);
        }

        void OpenDoorChapter()
        {
            door.DOScaleY(0, 5f).SetEase(Ease.InOutSine);
            door.DOScaleZ(0, 5f).SetEase(Ease.InOutSine);
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

                CastleSagamap c = hit.transform.GetComponent<CastleSagamap>();
                c.Select();
                lastCastle = c;
            }
        }

        public void OnSwipeUp(float force)
        {
            if (blockSwipe) return;

            if (currentRotation.x + moveOffset > bornes.x)
            {
                float v = currentRotation.x;

                mapRot.Value = bornes.x;
                mapCharger.ChunkIndex = Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);

                DOTween.To(
                    () => currentRotation,
                    (x) => currentRotation = x,
                    new Vector3(bornes.x, (currentRotation.y), (currentRotation.z)),
                    moveSpeed
                    );

                return;
            }

            mapRot.Value = currentRotation.x + (moveOffset + force / 10);
            mapCharger.ChunkIndex = Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);

            DOTween.To(
                () => currentRotation,
                (x) => currentRotation = x,
                new Vector3(currentRotation.x + (moveOffset + force / 10), (currentRotation.y), (currentRotation.z)),
                moveSpeed
                );

            ciel.transform.DOLocalMoveY(ciel.transform.position.y - 30f, moveSpeed);

        }

        public void OnSwipeDown(float force)
        {
            if (blockSwipe) return;

            if (currentRotation.x - moveOffset < bornes.y)
                return;

            mapRot.Value = currentRotation.x - (moveOffset + force / 10);
            mapCharger.ChunkIndex =Mathf.Abs(Mathf.RoundToInt(mapRot.Value) / 180);

            DOTween.To(
                () => currentRotation,
                (x) => currentRotation = x,
                new Vector3(currentRotation.x - (moveOffset + force / 10), (currentRotation.y), (currentRotation.z)),
                moveSpeed
                );

            ciel.transform.DOLocalMoveY(ciel.transform.position.y + 30f, moveSpeed);
        }

        public bool blockSwipe = false;
        public void BlockSwipe(bool value) => blockSwipe = value;
    }
}