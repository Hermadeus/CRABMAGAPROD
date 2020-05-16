using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CrabMaga
{
    public class MenuChoixUnitSelectionTile : UIDraggable
    {
        public CrabUnitData entitydata = default;

        public float dragValueTest = 25;
        public UnitSlot[] slots;
        public Image thumbnail = default;

        float x;

        UnitSlot targetSlot;

        int sibling;
        public Transform parentNull;
        Transform parent;


        public override void Init()
        {
            base.Init();

            thumbnail.sprite = entitydata.wheelThumbnail;
            parent = transform.parent;
            sibling = transform.GetSiblingIndex();

            if (entitydata.isLock)
            {
                gameObject.SetActive(false);
                gameObject.transform.parent = null;
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            x = 1000f;            
        }

        public override void OnDrag(PointerEventData eventData)
        {
            if (entitydata.isLock)
            {


                return;
            }


            base.OnDrag(eventData);

            for (int i = 0; i < slots.Length; i++)
            {
                x = Vector2.Distance(slots[i].rectTransform.position, rectTransform.position);
                slots[i].IsTarget = false;

                if (x < dragValueTest)
                {
                    slots[i].IsTarget = true;
                    targetSlot = slots[i];
                    //Debug.Log(targetSlot);
                }
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                x = Vector2.Distance(slots[i].rectTransform.position, rectTransform.position);
                slots[i].IsTarget = false;

                if (x < dragValueTest)
                {
                    slots[i].IsTarget = true;
                    targetSlot = slots[i];

                    targetSlot.AttributeData(entitydata);
                    targetSlot.IsTarget = false;

                }
                else
                {
                    targetSlot = null;
                    slots[i].IsTarget = false;
                }
            }
            

            targetSlot = null;

            base.OnEndDrag(eventData);
        }

        public void Desolidarise()
        {
            transform.parent = parentNull;
        }


        public void Resolidarise()
        {
            transform.parent = parent;
            transform.SetSiblingIndex(sibling);
        }

        
    }
}