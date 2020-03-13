using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviours/Movement/TargetMovement")]
    public class TargetMovement : SimpleMovement
    {
        public override void Move(Unit unit)
        {
            if (unit.asSurround) return;

            if (unit.UnitTarget == null)
            {
                GoAllRight(unit);
            }
            else
            {
                if (Vector3.Distance(unit.UnitTarget.transform.position, unit.transform.position) > unit.unitData.attackRange)
                {
                    GoAllRight(unit);
                    unit.transform.DOLookAt(unit.UnitTarget.transform.position, BaseRotationSpeed).SetEase(ease);
                }
                else if (Vector3.Distance(unit.UnitTarget.transform.position, unit.transform.position) < unit.unitData.attackRange)
                {
                    unit.asSurround = true;
                    unit.surroundBehaviour?.Surround(unit, unit.UnitTarget.transform);
                }
            }
        }
    }
}