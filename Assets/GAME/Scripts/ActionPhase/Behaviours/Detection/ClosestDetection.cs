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

            _unit.unitInRangeOfView = Physics.OverlapSphere(_unit.position, _unit.DetectionRange, _unit.layerMaskTarget);
            if (_unit.unitInRangeOfView.Length == 0)
                return;

            int x = 0;
            float bestDist = 1000f;

            for (int i = 0; i < _unit.unitInRangeOfView.Length; i++)
            {
                if(_unit.unitInRangeOfView[i].GetComponentInParent<Unit>().entityType == _unit.favoriteTarget)
                {
                    _unit.Target = _unit.unitInRangeOfView[i].GetComponentInParent<Unit>();
                    return;
                }
                else if (Vector3.Distance(_unit.transform.position, _unit.unitInRangeOfView[i].transform.position) < bestDist)
                {
                    x = i;
                    bestDist = Vector3.Distance(_unit.transform.position, _unit.unitInRangeOfView[i].transform.position);
                }
            }

            _unit.Target = _unit.unitInRangeOfView[x].GetComponentInParent<Unit>();
        }
    }
}