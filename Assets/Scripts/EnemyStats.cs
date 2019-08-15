using System;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [SerializeField] private Bar healthBar;
    [SerializeField] private Bar shieldBar;
    
    private float currentHealth;
    private float currentShield;
    private int enemyId;
    [SerializeField] private float maxHealth;
    [SerializeField] private float maxShield;
    [SerializeField] private float timeToShieldRecovery;
    [SerializeField] private float shieldRecoverySpeed;
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameManager.typeOfDamage damageResistance;

    public int EnemyId { get { return enemyId; } set { enemyId = value; } }

    public float Speed { get { return speed; } set { speed = value; } }
    
    public float MaxSpeed { get { return maxSpeed; }}

    public float RotationSpeed { get { return rotationSpeed; } }
    
    public GameManager.typeOfDamage DamageResistance { get { return damageResistance; } }

    public float TimeToShieldRecovery
    {
        get { return timeToShieldRecovery; }
        set { timeToShieldRecovery = value; }
    }

    public float ShieldRecoverySpeed { get { return shieldRecoverySpeed; } }

    public float MaxHealth
    {
        set
        {
            maxHealth = value;
            healthBar.MaxValue = MaxHealth;
        }
        get { return maxHealth; }
    }
    
    public float MaxShield
    {
        set
        {
            maxShield = value;
            shieldBar.MaxValue = MaxShield;
        }
        get { return maxShield; }
    }
    
    public float CurrentHealth
    {
        set
        {
            currentHealth = value;
            healthBar.CurrentValue = currentHealth;
        }
        get { return currentHealth; }
    }
    
    public float CurrentShield
    {
        set
        {
            currentShield = value;
            shieldBar.CurrentValue = currentShield;
        }
        get { return currentShield; }
    }

    public void Init()
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;

        MaxShield = maxShield;
        CurrentShield = MaxShield;

        Speed = maxSpeed;
    }
}
