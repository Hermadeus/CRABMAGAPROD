using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Movement/Null Movement")]
    public class NullMovement : BaseMovement
    {
        public override void Move(Entity _entity)
        {
        }
    }
}