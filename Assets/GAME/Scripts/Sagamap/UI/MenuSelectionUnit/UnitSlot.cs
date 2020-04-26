using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using QRTools.UI;

namespace CrabMaga
{
    public class UnitSlot : UIElement
    {
        public PlayerData playerData = default;

        public CrabUnitData currentEntityData = default;

        public Outline outline = default;

        public Image thumbnail = default;

        public int index = 0;

        bool isTarget = false;
        public bool IsTarget
        {
            get
            {
                return isTarget;
            }
            set
            {
                isTarget = value;

                if(value == true)
                {
                    outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 1);
                }
                else
                {
                    outline.effectColor = new Color(outline.effectColor.r, outline.effectColor.g, outline.effectColor.b, 0);
                }
            }
        }

        public override void Init()
        {
            base.Init();

            if(index == 1)
            {
                currentEntityData = playerData.entityData_slot01;
            }
            else if (index == 2)
            {
                currentEntityData = playerData.entityData_slot02;
            }
            else if (index == 3)
            {
                currentEntityData = playerData.entityData_slot03;
            }
            else if (index == 4)
            {
                currentEntityData = playerData.entityData_slot04;
            }

            ResetTile();
        }

        public void AttributeData(CrabUnitData data)
        {
            currentEntityData = data;

            if (index == 1)
            {
                playerData.entityData_slot01 = data;
            }
            else if (index == 2)
            {
                playerData.entityData_slot02 = data;
            }
            else if (index == 3)
            {
                playerData.entityData_slot03 = data;
            }
            else if (index == 4)
            {
                playerData.entityData_slot04 = data;
            }

            ResetTile();
        }

        public void ResetTile()
        {
            thumbnail.sprite = currentEntityData.wheelThumbnail;
        }
    }
}