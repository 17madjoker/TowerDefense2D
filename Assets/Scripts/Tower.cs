using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRange;
    [SerializeField] private int price;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private typeOfDamage damageType;
    [SerializeField] private GameObject projectilePref;
    
    private GameObject enemyTarget;
    private Queue<GameObject> enemiesIntoRange = new Queue<GameObject>();
    private bool canAttack = true;
    private float timeToAttack = 0;
    
    public enum typeOfDamage
    { Bullet, Canon, Rocket }

    public float Range
    {
        get { return range; }
        private set
        {
            range = value;
            transform.GetChild(0).localScale = new Vector3(range, range, 1);
            GetComponent<CircleCollider2D>().radius = range / 2;
        }
    }
    
    public int Price { get { return price; } }
    
    public int Damage { get { return damage; } }
    
    public float AttackSpeed { get { return attackSpeed; } }
    
    public float RotationSpeed { get { return rotationSpeed; } }

    public GameObject EnemyTarget { get { return enemyTarget; } }

    private void Start()
    {
        Range = range;
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
        if (enemyTarget == null && enemiesIntoRange.Count > 0 && transform.childCount == 1)
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
                if (timeToAttack >= attackSpeed)
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
        obj.transform.rotation = Quaternion.Slerp(obj.transform.rotation, rotate, RotationSpeed * Time.deltaTime);
        
        float angleDifference = Quaternion.Angle(obj.transform.rotation, rotate);

        return angleDifference;
    }

    public void Select()
    {
        spriteRange.enabled = !spriteRange.enabled;
    }
}
