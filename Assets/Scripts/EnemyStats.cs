using System;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [SerializeField]
    private HealthBar healthBar;
    
    private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    
    public float Speed { get { return speed; } }
    public float RotationSpeed { get { return rotationSpeed; } }

    public float MaxHealth
    {
        set
        {
            maxHealth = value;
            healthBar.MaxHealth = MaxHealth;
        }
        get { return maxHealth; }
    }
    public float CurrentHealth
    {
        set
        {
            currentHealth = value;
            healthBar.CurrentHealth = currentHealth;
        }
        get { return currentHealth; }
    }

    public void Init()
    {
        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;
    }
}
