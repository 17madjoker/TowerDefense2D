using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(4)]
public class Enemy : MonoBehaviour
{
    private List<Transform> wayPoints;
    private List<Debuff> debuffs;
    private List<Debuff> newDebuffs;
    private List<Debuff> removeDebuffs;
    private int wayPointIndex = 0;
    private float recoveryShieldTimer = 0;
    private Animator animator;
    private bool isDead = false;
    private bool isStop = false;
    private bool isStun = false;
    private float timeToStart = 0;
    public float currentProjectileSpeed;

    public bool IsStop { get { return isStop; } set { isStop = value; } }

    public bool IsStun { get { return isStun; } set { isStun = value; } }

    public bool IsDead { get { return isDead; } }

    [SerializeField] private EnemyStats enemyStats;
    [SerializeField] private GameObject floatingDamagePref;

    private void Awake()
    {
        enemyStats.Init();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        wayPoints = GameObject.Find("MapManager").GetComponent<MapManager>().WayPoints;
        
        newDebuffs = new List<Debuff>();
        debuffs = new List<Debuff>();
        removeDebuffs = new List<Debuff>();

        SetStartPosition();
    }

    private void Update()
    {
        if (!isDead)
        {
            CheckHealth();
            RecoveryShield();
            
            if (!IsStop)
                Move();
            
            HandleDebuffs();
        }
    }

    private void Move()
    {
        Vector2 direction;
        float distanceToWaypoint;
        float angel;
        Quaternion rotate;
        
        direction = wayPoints[wayPointIndex].position - transform.position;
        distanceToWaypoint = Vector2.Distance(transform.position, wayPoints[wayPointIndex].position);
     
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].position, enemyStats.Speed * Time.deltaTime);
        
        angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotate = Quaternion.AngleAxis(angel, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, enemyStats.RotationSpeed * Time.deltaTime);
        
        if (distanceToWaypoint <= 0.01f)
        {
            if (wayPointIndex < wayPoints.Count - 1)
            {
                wayPointIndex++;
            }
            else
            {
                EnemyDie("deathOnFinalTile");
                GameObject.Find("GameManager").GetComponent<GameManager>().BaseHealth -= 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy neighbor = other.GetComponent<Enemy>();

            if (enemyStats.EnemyId < neighbor.GetEnemyId())
                neighbor.IsStop = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy neighbor = other.GetComponent<Enemy>();

            if (enemyStats.EnemyId < neighbor.GetEnemyId())
                neighbor.IsStop = true;
        }
        
        else if (!IsStun && IsStop && !other.CompareTag("Enemy"))
        {
            timeToStart += Time.deltaTime;
        
            if (timeToStart >= 1f)
            {
                IsStop = false;
                timeToStart = 0;
            }
        }

    }

    private void SetStartPosition()
    {
        transform.position = wayPoints[0].position;
        
        Vector2 direction = wayPoints[1].position - transform.position;
        float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angel, Vector3.forward);

        StartCoroutine(EnterExitEffect());
    }

    private IEnumerator EnterExitEffect()
    {
        float size = 0;

        while (size <= 1)
        {
            transform.localScale = Vector3.Lerp(new Vector3(0.7f, 0.7f), new Vector3(1, 1), size);
               
            size += Time.deltaTime * 2;
            yield return null;
        }
    }

    public void TakeDamage(float damage, GameManager.typeOfDamage projectileDamageType)
    {
        if (enemyStats.DamageResistance == projectileDamageType)
            damage *= 0.8f;
        
        CalculateDamage(damage);
    }

    private void CalculateDamage(float damage)
    {
        if (enemyStats.CurrentShield > 0)
        {
            damage *= 0.8f;

            if (damage <= enemyStats.CurrentShield)
                enemyStats.CurrentShield -= damage;

            else if (damage > enemyStats.CurrentShield)
            {
                float restDamage = damage - enemyStats.CurrentShield;
                enemyStats.CurrentShield -= enemyStats.CurrentShield;
                enemyStats.CurrentHealth -= restDamage;
            }
        }

        else
            enemyStats.CurrentHealth -= damage;

        recoveryShieldTimer = 0;
    }
    
    private void RecoveryShield()
    {
        if (enemyStats.CurrentShield != enemyStats.MaxShield)
        {
            if (recoveryShieldTimer >= enemyStats.TimeToShieldRecovery)
            {
                enemyStats.CurrentShield = Mathf.Lerp(enemyStats.CurrentShield, enemyStats.MaxShield, enemyStats.ShieldRecoverySpeed * Time.deltaTime);
            }

            recoveryShieldTimer += Time.deltaTime;
        }
    }

    public void DamageHealth(float damage)
    {
        enemyStats.CurrentHealth -= damage;
    }

    public void SetTimeToShieldRecovery(float value) { enemyStats.TimeToShieldRecovery = value; }
    
    public float GetTimeToShieldRecovery() { return enemyStats.TimeToShieldRecovery; }

    public void SetEnemyId(int id) { enemyStats.EnemyId = id; }

    public int GetEnemyId() { return enemyStats.EnemyId; } 
    
    private void CheckHealth()
    {
        if (enemyStats.CurrentHealth <= 0f && !isDead)
            EnemyDie("deathOnLine");
    }

    private void EnemyDie(string trigger)
    {
        animator.SetTrigger(trigger);
        isDead = true;
    }

    public void FloatingDamage(float damage, Transform enemyTransform, string damageType, float hideSpeed = 1.5f)
    {       
        FloatingDamage floatingDamage = Instantiate(floatingDamagePref).transform.GetChild(0).GetComponent<FloatingDamage>();

        floatingDamage.DamageType = damageType;
        floatingDamage.Damage = damage;
        floatingDamage.EnemyTransform = enemyTransform;
        floatingDamage.HideSpeed = hideSpeed;
    }

    public void AddDebuff(Debuff newDebuff)
    {
        if (!debuffs.Exists(x => x.Type == newDebuff.Type))
            newDebuffs.Add(newDebuff);
        
        else if (debuffs.Exists(x => x.Type == newDebuff.Type))
            debuffs.Find(x => x.Type == newDebuff.Type).DurationTimer = 0;
    }

    public void RemoveDebuff(Debuff debuff)
    {
        removeDebuffs.Add(debuff);
    }

    public void HandleDebuffs()
    {
        if (newDebuffs.Count > 0)
        {
            debuffs.AddRange(newDebuffs);
            newDebuffs.Clear();
        }

        foreach (Debuff debuff in removeDebuffs)
        {
            debuffs.Remove(debuff);
        }
        
        removeDebuffs.Clear();
        
        foreach (Debuff debuff in debuffs)
        {
            debuff.ApplyDebuff();
        }
    }
}
