using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RocketTowerUpgrade : TowerUpgrade
{
    [SerializeField] private float damagePerSecondIncrease;

    public float DamagePerSecondIncrease { get { return damagePerSecondIncrease; } }
}
