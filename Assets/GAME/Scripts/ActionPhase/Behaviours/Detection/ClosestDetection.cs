using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrabMaga
{
    [CreateAssetMenu(menuName = "CRAB MAGA/Behaviour/Detection/Closest Detection")]
    public class ClosestDetection : BaseDetection
    {
        public override void Detect(Unit _unit)
        {
            base.Detect(_unit);
            
            _unit.UnitInRangeOfView = Physics.OverlapSphere(_unit.position, _unit.DetectionRange, _unit.layerMaskTarget);
            //if (_unit.UnitInRangeOfView.Length == 0)
            //    return;

            //int x = 0;
            //float bestDist = 1000f;

            //for (int i = 0; i < _unit.UnitInRangeOfView.Length; i++)
            //{
            //    if(_unit.UnitInRangeOfView[i].GetComponentInParent<Unit>().entityType == _unit.favoriteTarget)
            //    {
            //        Unit t = _unit.UnitInRangeOfView[i].GetComponentInParent<Unit>();
            //        if (t.attackedBy != null)
            //            return;

            //        _unit.Target = t;
            //        return;
            //    }
            //    else if (Vector3.Distance(_unit.transform.position, _unit.UnitInRangeOfView[i].transform.position) < bestDist)
            //    {
            //        x = i;
            //        bestDist = Vector3.Distance(_unit.transform.position, _unit.UnitInRangeOfView[i].transform.position);
            //    }
            //}

            //Unit _t = _unit.UnitInRangeOfView[x].GetComponentInParent<Unit>();

            //if (_t.attackedBy != null)
            //    return;

            //_unit.Target = _t;
        }
    }
}