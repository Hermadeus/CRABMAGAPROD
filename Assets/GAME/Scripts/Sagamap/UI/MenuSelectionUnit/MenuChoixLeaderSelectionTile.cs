using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace CrabMaga
{
    public class MenuChoixLeaderSelectionTile : UIDraggable
    {
        public LeaderData entitydata = default;

        public float dragValueTest = 25;
        public LeaderSlot slot;
        public Image thumbnail = default;

        float x;

        LeaderSlot targetSlot;

        int sibling;
        public Transform parentNull;
        Transform parent;

        public CodeColor CodeColor;
        public TextMeshProUGUI lvl;
        public Image lvlBack;

        public override void Init()
        {
            base.Init();

            thumbnail.sprite = entitydata.thumbnail;
            parent = transform.parent;
            sibling = transform.GetSiblingIndex();

            lvl.text = entitydata.currentLevel.ToString();
            lvlBack.color = CodeColor.GetColor(entitydata.Triforce);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            x = 1000f;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            x = Vector2.Distance(slot.rectTransform.position, rectTransform.position);
            //Debug.Log(x);
            slot.IsTarget = false;

            if (x < dragValueTest)
            {
                slot.IsTarget = true;
                targetSlot = slot;
                Debug.Log(targetSlot);
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            x = Vector2.Distance(slot.rectTransform.position, rectTransform.position);
            slot.IsTarget = false;

            if (x < dragValueTest)
            {
                slot.IsTarget = true;
                targetSlot = slot;

                targetSlot.AttributeData(entitydata);
                targetSlot.IsTarget = false;

            }
            else
            {
                targetSlot = null;
                slot.IsTarget = false;
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