using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletTowerUpgrade : TowerUpgrade
{
    [SerializeField] private float multiplierIncrease;

    public float MultiplierIncrease { get { return multiplierIncrease; } }
}
