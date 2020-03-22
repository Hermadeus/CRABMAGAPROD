using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using QRTools.UI;

namespace CrabMaga
{
    public class UnitWheelSlot : UIElement
    {
        public EntityData entityDataRef = default;

        public override void Init()
        {

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
            Debug.Log("select " + gameObject.name);
        }
    }
}