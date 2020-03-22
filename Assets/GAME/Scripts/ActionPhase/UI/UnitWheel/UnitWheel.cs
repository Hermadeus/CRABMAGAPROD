using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Sirenix.OdinInspector;

using QRTools.UI;
using QRTools.Inputs;

namespace CrabMaga
{
    public class UnitWheel : UIElement
    {
        [BoxGroup("References")]
        public InputTouch UnitWheelInput = default;
        [BoxGroup("References")]
        public PlayerData playerData = default;

        [BoxGroup("Properties")]
        public bool rightHand = true;

        [FoldoutGroup("Slots")]
        public UnitWheelSlot
            slot01,
            slot02,
            slot03,
            slot04;

        public UnitWheelSlot currentSelectedSlot;

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        public override void Init()
        {
            if (!rightHand)
                rectTransform.localScale = new Vector3(rectTransform.localScale.x * -1, rectTransform.localScale.y, rectTransform.localScale.z);

            InitSlots();

            m_Raycaster = GetComponent<GraphicRaycaster>();
            m_EventSystem = GetComponent<EventSystem>();
        }

        public override void Show()
        {
            base.Show();

            rectTransform.localPosition = new Vector2(UnitWheelInput.InputCurrentPosition.x - (Screen.width / 2), UnitWheelInput.InputCurrentPosition.y - (Screen.height / 2));
        }

        public override void Hide()
        {
            base.Hide();

            if (currentSelectedSlot != null)
            {
                currentSelectedSlot.OnSelect();
                currentSelectedSlot = null;
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
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<UnitWheelSlot>())
                    currentSelectedSlot = result.gameObject.GetComponent<UnitWheelSlot>();
            }
        }
    }
}