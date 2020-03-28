using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class UnitWheelSlot : UIElement
    {
        public EntityData entityDataRef = default;

        bool isSelected = false;
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                if (value)
                {
                    if(background != null)
                        background.color = Color.red;
                }
                else
                {
                    if (background != null)
                        background.color = Color.white;
                }
            }
        }

        public override void Init()
        {
            base.Init();
        }

        public void InitSlot(EntityData _entityData)
        {
            if(_entityData != null)
                entityDataRef = _entityData;
            else
            {
                Hide();
            }
        }

        public void OnSelect()
        {
            //Debug.Log("select " + gameObject.name);
        }
    }
}