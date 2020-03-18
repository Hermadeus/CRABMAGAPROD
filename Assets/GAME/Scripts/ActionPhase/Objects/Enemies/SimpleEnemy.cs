using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using Sirenix.OdinInspector;

namespace CrabMaga
{
    public class SimpleEnemy : Unit
    {
        public GuardHouseManager guardHouseManager = default;
        [ReadOnly] public GuardHouse guardhouseTarget = default;

        private Tween doMoveTween = default;

        protected override void Init()
        {
            base.Init();
            guardhouseTarget = guardHouseManager.GetNotOccupyGuardHouse();

            if (guardHouseManager != null)
            {
                float s = Vector3.Distance(transform.position, guardHouseManager.transform.position);
                s /= Speed;
                doMoveTween = transform.DOMove(guardhouseTarget.transform.position, s).
                    SetEase(Ease.Linear).
                    OnComplete(ReachGuardHouse);
            }
        }

        protected override void UpdateComportement()
        {
            base.UpdateComportement();
            CheckIfTarget();
        }

        void CheckIfTarget()
        {
            if (UnitTarget != null)
            {
                doMoveTween.Kill();
                doMoveTween = null;
            }
        }

        void ReachGuardHouse()
        {
            movementBehaviour.StopMove(this);
        }

        public override void Push()
        {
            base.Push();
            Destroy(gameObject);
        }
    }
}