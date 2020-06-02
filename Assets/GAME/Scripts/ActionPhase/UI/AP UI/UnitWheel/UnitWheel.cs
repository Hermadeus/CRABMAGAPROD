using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Sirenix.OdinInspector;

using QRTools.UI;
using QRTools.Inputs;
using QRTools.Utilities.Observer;

namespace CrabMaga
{
    public class UnitWheel : UIElement, IObservable
    {
        [BoxGroup("References")]
        public InputTouch UnitWheelInput = default;
        [BoxGroup("References")]
        public PlayerData playerData = default;
        [BoxGroup("References")]
        public PoolingManager poolingManager = default;
        [BoxGroup("References")]
        public AP_GameManager AP_GameManager = default;
        [BoxGroup("References")]
        public CameraSlider cameraSlider = default;

        [FoldoutGroup("Slots")]
        public UnitWheelSlot
            slot01,
            slot02,
            slot03,
            slot04;

        public UnitWheelGeneralSlot
            slotGeneral;

        UnitWheelSlot previousSelectedSlot;
        [SerializeField, ReadOnly] UnitWheelSlot currentSelectedSlot;
        public UnitWheelSlot CurrentSelectedSlot
        {
            get => currentSelectedSlot;
            set
            {                
                currentSelectedSlot = value;

                if(value != previousSelectedSlot)
                {
                    SetSelectAllSlot(false);

                    if(CurrentSelectedSlot != null)
                        CurrentSelectedSlot.IsSelected = true;

                    previousSelectedSlot = value;
                }
            }
        }


        private bool isBlocked = false;
        public bool IsBlocked
        {
            get => isBlocked;
            set
            {
                isBlocked = value;
                if(value)
                    Hide();
            }
        }

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        public LineSystem lineSystem;
        int line;

        public LeaderToken token;

        public InputTouch simpleTouch;

        public override void Init()
        {
            base.Init();

            if (!playerData.RightHand)
                rectTransform.localScale = new Vector3(rectTransform.localScale.x * -1, rectTransform.localScale.y, rectTransform.localScale.z);

            InitSlots();

            m_Raycaster = GetComponent<GraphicRaycaster>();
            m_EventSystem = GetComponent<EventSystem>();

            Add(playerData);

            UnitWheelInput.onTouchEnter.AddListener(FindLine);
            UnitWheelInput.onTouchEnd.AddListener(Unlock);

            //simpleTouch.onTouchEnter.AddListener(Show);
            //simpleTouch.onTouchEnter.AddListener(Unlock);
        }

        public void FindLine()
        {
            if (IsBlocked)
                return;

            line = Mathf.RoundToInt(UnitWheelInput.RayPoint.x);
        }

        public void Unlock() => IsBlocked = false;

        public override void Show()
        {
            if (IsBlocked)
                return;


            base.Show();
            rectTransform.localPosition = new Vector2(
                UnitWheelInput.InputCurrentPosition.x - (Screen.width / 2),
                UnitWheelInput.InputCurrentPosition.y - (Screen.height / 2));
        }

        public override void Hide()
        {
            base.Hide();

            if (CurrentSelectedSlot != null)
            {
                if(CurrentSelectedSlot is UnitWheelGeneralSlot)
                {
                    poolingManager.InvokeLeader(new Vector3(UnitWheelInput.RayPoint.x,
                            0,
                            AP_GameManager.CurrentInstantiationZone.transform.position.z)
                            );

                    ((UnitWheelGeneralSlot)CurrentSelectedSlot).Desactive();
                    token.Show();
                }else
                    InstantiateFormation();

                CurrentSelectedSlot.OnSelect();
                CurrentSelectedSlot.IsSelected = false;
                CurrentSelectedSlot = null;
            }

            lineSystem.DeselectAll();
        }

        public void InitSlots()
        {
            slot01.InitSlot(playerData.entityData_slot01);
            slot02.InitSlot(playerData.entityData_slot02);
            slot03.InitSlot(playerData.entityData_slot03);
            slot04.InitSlot(playerData.entityData_slot04);

            slotGeneral.InitSlot(playerData.leader_slot);
        }

        private void Update()
        {
            if (slot01.IsSelected == false && slot02.IsSelected == false && slot03.IsSelected == false && slot04.IsSelected == false && slotGeneral.IsSelected == false)
            {
                CurrentSelectedSlot = null;
            }                        
        }

        public void CheckSlot()
        {
            if (AP_GameManager.InPause)
                return;

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            if (UnitWheelInput.Touches.Length > 0)
            {
                if(!IsBlocked)
                    lineSystem.SelectLine(line);
            }

            if (results.Count == 0)
            {
                SetSelectAllSlot(false);
                return;
            }

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<UnitWheelSlot>() || result.gameObject.GetComponent<UnitWheelSlot>())
                {
                    CurrentSelectedSlot = result.gameObject.GetComponent<UnitWheelSlot>();

                    if (!CurrentSelectedSlot.IsSelected)
                        CurrentSelectedSlot.IsSelected = true;
                }
            }
        }

        public void InstantiateFormation()
        {
            poolingManager.CreateCrabFormationWithType(
                CurrentSelectedSlot.entityDataRef.unitType.GetType(),
                new Vector3(UnitWheelInput.RayPoint.x,
                            0,
                            AP_GameManager.CurrentInstantiationZone.transform.position.z),
                CurrentSelectedSlot.entityDataRef.formationX,
                CurrentSelectedSlot.entityDataRef.formationY,
                CurrentSelectedSlot.entityDataRef.density
                );
        }

        void SetSelectAllSlot(bool state)
        {
            slot01.IsSelected = state;
            slot02.IsSelected = state;
            slot03.IsSelected = state;
            slot04.IsSelected = state;
            slotGeneral.IsSelected = state;
        }

        public void Add(IObserver observer)
        {
            playerData.Observables.Add(this);
        }

        public void Remove(IObserver observer)
        {
            throw new System.NotImplementedException();
        }

        public void Notify()
        {
            ChangeHand();
        }

        void ChangeHand()
        {
            if (playerData.RightHand)
            {
                rectTransform.localScale = new Vector3(1, rectTransform.localScale.y, rectTransform.localScale.z);
            }
            else
            {
                rectTransform.localScale = new Vector3(-1, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }

        public void Blocked(bool state) => IsBlocked = state;
    }
}