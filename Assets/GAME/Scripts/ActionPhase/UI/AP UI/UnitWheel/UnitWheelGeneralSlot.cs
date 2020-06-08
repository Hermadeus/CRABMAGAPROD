using DG.Tweening;
using QRTools.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrabMaga
{
    public class UnitWheelGeneralSlot : UnitWheelSlot
    {
        public LeaderData generalDataRef = default;

        public void InitSlot(LeaderData _entityData)
        {
            if (_entityData != null)
            {
                generalDataRef = _entityData;
                crabThumbnailImage.sprite = _entityData.wheelThumbnail;
                Show();
            }
            else
                Hide();
        }

        public void Desactive() => gameObject.SetActive(false);
    }
}