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
            
            _unit.UnitInRangeOfView = Physics.OverlapSphere(_unit.transform.position, _unit.DetectionRange, _unit.layerMaskTarget);

            if (_unit.UnitInRangeOfView.Length > 0)
            {
                float _closestDist = 1000f;
                int _index = 0;

                for (int i = 0; i < _unit.UnitInRangeOfView.Length; i++)
                {
                    if (Vector3.Distance(_unit.UnitInRangeOfView[i].transform.position, _unit.transform.position) < _closestDist)
                    {
                        if (_unit.UnitInRangeOfView[i].GetComponentInParent<Crablinde>())
                        {
                            //_unit.Target = _unit.UnitInRangeOfView[i].GetComponentInParent<Unit>();

                            _unit.UnitInRangeOfView[i].GetComponentInParent<Crablinde>().animator.SetTrigger("onUlt");
                            Debug.Log("Je detecte un crablinde");
                        }

                        _index = i;
                        _closestDist = Vector3.Distance(_unit.UnitInRangeOfView[i].transform.position, _unit.transform.position);
                    }
                }

                _unit.Target = _unit.UnitInRangeOfView[_index].GetComponentInParent<Unit>();
                               
                if (_unit.pastilleRef != null)
                {
                    _unit.pastilleRef.AnimationOnDetection( _unit.entityData);
                }
            }
        }
    }
}