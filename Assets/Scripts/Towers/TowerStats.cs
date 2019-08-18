using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[Serializable]
public class TowerStats
{
    [SerializeField] private Tower tower;

    [SerializeField] private int level = 1;
    [SerializeField] private int price;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameManager.typeOfDamage damageType;

    
    public float Range
    {
        get { return range; }
        set
        {
            range = value;
            tower.transform.GetChild(0).localScale = new Vector3(range, range, 1);
            tower.GetComponent<CircleCollider2D>().radius = range / 2;
        }
    }

    public int Price { get { return price; } set { price = value; } }

    public int Damage { get { return damage; } set { damage = value; } }

    public float AttackSpeed { get { return attackSpeed; } }
    
    public float RotationSpeed { get { return rotationSpeed; } }
    
    public GameManager.typeOfDamage DamageType { get { return damageType; } }

    public int Level { get { return level; } set { level = value; } }

    public void Init()
    {
        Range = range;
    }
}
