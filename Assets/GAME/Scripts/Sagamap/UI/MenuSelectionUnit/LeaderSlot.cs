using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QRTools.UI;

namespace CrabMaga
{
    public class LeaderSlot : UIElement
    {
        public PlayerData playerData = default;

        public LeaderData currentEntityData = default;

        public Outline outline = default;

        public Image thumbnail = default;

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

                if (value == true)
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

            currentEntityData = playerData.leader_slot;
            ResetTile();
        }

        public void AttributeData(LeaderData data)
        {
            currentEntityData = data;

            playerData.leader_slot = data;

            ResetTile();
        }

        public void ResetTile()
        {
            if (currentEntityData == null) return;

            thumbnail.sprite = currentEntityData.thumbnail;
        }
    }
}