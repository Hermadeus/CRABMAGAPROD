using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using QRTools.UI;
using QRTools.Inputs;
using QRTools.Variables;

namespace CrabMaga
{
    public class LeaderToken : UIElement
    {
        public bool isSelected = false;
        public InputTouch inputTouchToken = default;
        public LeaderEffectEvent leaderEffectEvent = default;
        public PoolingManager poolingManager = default;
        public AP_GameManager gameManager = default;

        public Sprite ultiSprite = default;
        public Sprite noneSprite = default;

        enum StateToken { UNIT, ULTI, NONE}
        StateToken stateToken = StateToken.UNIT;

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        public override void Init()
        {
            base.Init();

            inputTouchToken.onTouchEnter.AddListener(OnInput);
            inputTouchToken.onTouchMoving.AddListener(IsSelected);
            inputTouchToken.onTouchEnd.AddListener(OnRelease);

            rectTransform = GetComponent<RectTransform>();

            m_Raycaster = GetComponent<GraphicRaycaster>();
            m_EventSystem = GetComponent<EventSystem>();
        }

        public void OnInput()
        {
            m_PointerEventData = new PointerEventData(m_EventSystem);
            m_PointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            m_Raycaster.Raycast(m_PointerEventData, results);

            if (results.Count == 0)
            {
                return;
            }

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.GetComponent<LeaderToken>())
                {
                    isSelected = true;
                }
            }
        }

        public void IsSelected()
        {
            switch (stateToken)
            {
                case StateToken.UNIT:
                    if (isSelected)
                    {
                        rectTransform.localPosition = new Vector2(
                            inputTouchToken.InputCurrentPosition.x - ((Screen.width / 2) + (rectTransform.sizeDelta.x / 2)),
                            inputTouchToken.InputCurrentPosition.y - ((Screen.height / 2) + (rectTransform.sizeDelta.y / 2)));
                    }
                    break;
                case StateToken.ULTI:
                    break;
                case StateToken.NONE:
                    break;
            }            
        }

        public void OnRelease()
        {
            switch (stateToken)
            {
                case StateToken.UNIT:
                    if (isSelected)
                    {
                        rectTransform.anchoredPosition = Vector2.zero;
                        isSelected = false;

                        poolingManager.InvokeLeader(inputTouchToken);

                        GetComponent<Image>().sprite = ultiSprite;
                        stateToken = StateToken.ULTI;
                    }
                    break;
                case StateToken.ULTI:

                    GetComponent<Image>().sprite = noneSprite;
                    gameManager.leaderOnBattle.UsePassif();
                    stateToken = StateToken.NONE;

                    break;
                case StateToken.NONE:
                    break;
            }
            
        }
    }
}