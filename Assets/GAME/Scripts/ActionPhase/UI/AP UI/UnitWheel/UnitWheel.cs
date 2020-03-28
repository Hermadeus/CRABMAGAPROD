﻿using System.Collections;
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

        [FoldoutGroup("Slots")]
        public UnitWheelSlot
            slot01,
            slot02,
            slot03,
            slot04;

        UnitWheelSlot previousSelectedSlot;
        [SerializeField] UnitWheelSlot currentSelectedSlot;
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

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;


        public override void Init()
        {
            base.Init();

            if (!playerData.rightHand)
                rectTransform.localScale = new Vector3(rectTransform.localScale.x * -1, rectTransform.localScale.y, rectTransform.localScale.z);

            InitSlots();

            m_Raycaster = GetComponent<GraphicRaycaster>();
            m_EventSystem = GetComponent<EventSystem>();

            Add(playerData);
        }

        public override void Show()
        {
            if (UnitWheelInput.objectHit.GetComponent<InstantiationZone>())
            {
                base.Show();
                rectTransform.localPosition = new Vector2(
                    UnitWheelInput.InputCurrentPosition.x - (Screen.width / 2),
                    UnitWheelInput.InputCurrentPosition.y - (Screen.height / 2));
            }
        }

        public override void Hide()
        {
            base.Hide();

            if (CurrentSelectedSlot != null)
            {
                InstantiateFormation();
                CurrentSelectedSlot.OnSelect();
                CurrentSelectedSlot.IsSelected = false;
                CurrentSelectedSlot = null;
            }
        }

        public void InitSlots()
        {
            slot01.InitSlot(playerData.entityData_slot01);
            slot02.InitSlot(playerData.entityData_slot02);
            slot03.InitSlot(playerData.entityData_slot03);
            slot04.InitSlot(playerData.entityData_slot04);
        }

        public void CheckSlot()
        {
            if (AP_GameManager.InPause)
                return;

            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            if(results.Count == 0)
            {
                SetSelectAllSlot(false);
                return;
            }

            if(slot01.IsSelected == false && slot02.IsSelected == false && slot03.IsSelected == false && slot04.IsSelected == false)
            {
                CurrentSelectedSlot = null;
            }

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<UnitWheelSlot>())
                {
                    CurrentSelectedSlot = result.gameObject.GetComponent<UnitWheelSlot>();
                    if (!CurrentSelectedSlot.IsSelected)
                        CurrentSelectedSlot.IsSelected = true;
                }
            }
        }

        public void InstantiateFormation()
        {
            //poolingManager.CreateCrabFormation(CurrentSelectedSlot.entityDataRef as CrabUnitData, UnitWheelInput.RayPoint);
            poolingManager.CreateCrabFormationWithType(
                CurrentSelectedSlot.entityDataRef.unitType.GetType(),
                UnitWheelInput.RayPoint
                );
        }

        void SetSelectAllSlot(bool state)
        {
            slot01.IsSelected = state;
            slot02.IsSelected = state;
            slot03.IsSelected = state;
            slot04.IsSelected = state;
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
            if (playerData.rightHand)
            {
                rectTransform.localScale = new Vector3(1, rectTransform.localScale.y, rectTransform.localScale.z);
            }
            else
            {
                rectTransform.localScale = new Vector3(-1, rectTransform.localScale.y, rectTransform.localScale.z);
            }
        }
    }
}