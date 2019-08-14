using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected TowerStats towerStats;
    [SerializeField] private SpriteRenderer spriteRange;
    [SerializeField] private GameObject projectilePref;
    
    private GameObject enemyTarget;
    private Queue<GameObject> enemiesIntoRange = new Queue<GameObject>();
    private bool canAttack = true;
    private float timeToAttack = 0;

    public GameObject EnemyTarget { get { return enemyTarget; } }
    public Tile Tile { get; set; }

    private void Awake()
    {
        towerStats.Init();
    }

    private void Update()
    {
        Attack();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            enemiesIntoRange.Enqueue(target.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            enemyTarget = null;
        }
    }

    private void Attack()
    {
        if (enemyTarget == null && enemiesIntoRange.Count > 0)
            enemyTarget = enemiesIntoRange.Dequeue();
        
        if (enemyTarget != null)
        {
            float angel = LookOnTarget(gameObject, enemyTarget);
        
            if (canAttack && angel <= 5f)
            {
                Shoot();
                canAttack = false;
            }
            
            else if (!canAttack)
            {
                if (timeToAttack >= towerStats.AttackSpeed)
                {
                    canAttack = true;
                    timeToAttack = 0;
                }

                timeToAttack += Time.deltaTime;
            }
        }
    }

    private void Shoot()
    {
        Projectile projectile = Instantiate(projectilePref).GetComponent<Projectile>();

        projectile.SetProjectile(this);
    }

    private float LookOnTarget(GameObject obj, GameObject target)
    {
        Vector3 attackDirection = target.transform.position - obj.transform.position;
        
        float angel = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
        Quaternion rotate = Quaternion.AngleAxis(angel, Vector3.forward);
        rotate *= Quaternion.Euler(0, 0, -90);
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotate, towerStats.RotationSpeed * Time.deltaTime);
        
        float angleDifference = Quaternion.Angle(obj.transform.rotation, rotate);

        return angleDifference;
    }

    public void Select() { spriteRange.enabled = !spriteRange.enabled; }

    public int GetTowerPrice() { return towerStats.Price; }

    public float GetTowerRange() { return towerStats.Range; }
    
    public float GetTowerDamage() { return towerStats.Damage; }

    public GameManager.typeOfDamage GetTowerDamageType() { return towerStats.DamageType; }

    public abstract Debuff CreateDebuff(Enemy target);

    public abstract int GetChance();
}
