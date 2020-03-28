﻿using System.Collections;
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
        [SerializeField] Transform startPos = default;
        public Transform StartPos { get => startPos; set => startPos = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] Transform endPos = default;
        public Transform EndPos { get => endPos; set => endPos = value; }

        [SerializeField] float laserSize = .04f;
        public float LaserSize { get => laserSize; set => laserSize = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] float laserChargeTimer = 1f;
        public float LaserChargeTime { get => laserChargeTimer; set => laserChargeTimer = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] LineRenderer lineRendererLaser = default;
        public LineRenderer LineRendererLaser { get => lineRendererLaser; set => lineRendererLaser = value; }

        [FoldoutGroup("Passif Attribute")]
        [SerializeField] Collider[] laserTarget;
        public Collider[] LaserTarget { get => laserTarget; set => laserTarget = value; }

        public void StartLaser()
        {
            DOTween.To(() => LineRendererLaser.startWidth, x => LineRendererLaser.startWidth = x, LaserSize, 1f).SetEase(Ease.InOutElastic);
            DOTween.To(() => LineRendererLaser.endWidth, x => LineRendererLaser.endWidth = x, LaserSize, 1f).SetEase(Ease.InOutElastic);
        }

        public void StopLaser()
        {
            DOTween.To(() => LineRendererLaser.startWidth, x => LineRendererLaser.startWidth = x, 0, 1f).SetEase(Ease.Linear);
            DOTween.To(() => LineRendererLaser.endWidth, x => LineRendererLaser.endWidth = x, 0, 1f).SetEase(Ease.Linear);

            IsStatic = false;
        }

        public void ChargeLaser()
        {
            IsStatic = true;
        }
    }
}