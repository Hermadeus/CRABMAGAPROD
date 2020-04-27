using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;

using QRTools.Inputs;
using QRTools.UI;

namespace CrabMaga
{
    public class UIDraggable : UIElement, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public InputTouch touch = default;

        bool isDrag = false;

        Vector2 startPos;

        public override void Init()
        {
            base.Init();

            startPos = rectTransform.anchoredPosition;

            //touch.onTouchEnter.AddListener(OnDragStart);
            //touch.onTouchMoving.AddListener(OnDrag);
            //touch.onTouchEnd.AddListener(OnDragEnd);
        }

        public virtual void OnBeginDrag(PointerEventData eventData)
        {

            isDrag = true;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            isDrag = false;
            //rectTransform.anchoredPosition = startPos;
        }
    }
}