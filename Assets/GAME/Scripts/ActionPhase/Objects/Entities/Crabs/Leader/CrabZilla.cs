using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Sirenix.OdinInspector;

using DG.Tweening;

namespace CrabMaga
{
    public class CrabZilla : Leader, ILaserAttacker
    {
        [FoldoutGroup("Passif Attribute")]
        [SerializeField] float laserDamage = 25f;
        public float LaserDamage { get => laserDamage; set => laserDamage = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] GameObject laser = default;
        public GameObject Laser { get => laser; set => laser = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] Transform source = default;
        public Transform Source { get => source; set => source = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] Transform mid = default;
        public Transform Mid { get => mid; set => mid = value; }

        float laserSize = .2f;
        public float LaserSize { get => laserSize; set => laserSize = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] float laserChargeTimer = 1f;
        public float LaserChargeTime { get => laserChargeTimer; set => laserChargeTimer = value; }

        public Collider[] colli;

        public void StartLaser()
        {
            DOTween.To(() => LaserSize, x => LaserSize = x, .2f, 1f).SetEase(Ease.InOutElastic);
        }

        public void StopLaser()
        {
            DOTween.To(() => LaserSize, x => LaserSize = x, 0, 1f).SetEase(Ease.Linear);

            IsStatic = false;
        }

        public void ChargeLaser()
        {
            IsStatic = true;
        }
    }
}