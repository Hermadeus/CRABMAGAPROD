using System;
using UnityEngine;
using UnityEngine.Events;

namespace QRTools.Utilities
{
    public class RayCaster
    {
        public RaycastHit hit;

        public Vector3 RayPoint { get; private set; }
        public Collider objectHit { get; private set; }
        public Collider previousObjectHit { get; private set; }
        
        public UnityEvent<Collider> actionEnter;
        public UnityEvent<Collider> actionStay;
        public UnityEvent<Collider> actionExit;

        private bool asRayEnter = false;

        public RayCaster(UnityEvent<Collider> _actionEnter, UnityEvent<Collider> _actionStay, UnityEvent<Collider> _actionExit)
        {
            actionEnter = _actionEnter;
            actionStay = _actionStay;
            actionExit = _actionExit;

            objectHit = null;
            previousObjectHit = null;
        }

        public bool RayCast(Ray ray)
        {
            if (Physics.Raycast(ray, out hit))
            {
                RayPoint = hit.point;
                objectHit = hit.collider;

                if (objectHit != previousObjectHit)
                {
                    asRayEnter = false;
                    actionStay?.Invoke(objectHit);
                }

                if (!asRayEnter)
                {
                    actionEnter?.Invoke(objectHit);
                    previousObjectHit = objectHit;
                    asRayEnter = true;
                }

                actionExit?.Invoke(objectHit);
                return true;
            }
            return false;
        }
    }
}