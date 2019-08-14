using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileStats
{
    [SerializeField] private Tower tower;
    [SerializeField] private GameManager.typeOfDamage damageType;
    [SerializeField] private float speed;
    private float damage;

    public GameManager.typeOfDamage DamageType { get { return damageType; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public Tower Tower { get { return tower; } }

    public void Init()
    {
        damage = tower.GetTowerDamage();
    }
}
