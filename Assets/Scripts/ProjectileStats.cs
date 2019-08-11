using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileStats
{
    [SerializeField] private Tower tower;
    [SerializeField] private typeOfDamage damageType;
    [SerializeField] private float speed;
    private float damage;

    public typeOfDamage DamageType { get { return damageType; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    
    public enum typeOfDamage { Bullet, Canon, Rocket }

    public void Init()
    {
        damage = tower.GetTowerDamage();
    }
}
