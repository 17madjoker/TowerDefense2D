using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    private Animator animator;
    [SerializeField] private ProjectileStats projectileStats;

    private void Start()
    {
        animator = GetComponent<Animator>();
        projectileStats.Init();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (target.GetComponent<Enemy>().IsDead || target == null)
        {
            Destroy(gameObject);
            target = null;
        }
            
        else if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
        
            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotate = Quaternion.AngleAxis(angel, Vector3.forward);
            rotate *= Quaternion.Euler(0, 0, -90);
        
            transform.rotation = rotate;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, projectileStats.Speed * Time.deltaTime);
        }
    }

    public void SetProjectile(Tower parentTower)
    {
        target = parentTower.EnemyTarget;
        
        transform.position = parentTower.transform.position;
        transform.SetParent(parentTower.transform);
    }

    private void AddDebuff(Enemy enemy)
    {
        if (projectileStats.Tower.GetChance() >= Random.Range(1, 101))
        {
            Debuff debuff = projectileStats.Tower.CreateDebuff(enemy);
            enemy.AddDebuff(debuff);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (target.gameObject == other.gameObject)
            {
                Enemy enemy = target.GetComponent<Enemy>();
                float damage = projectileStats.Damage;
                
                enemy.TakeDamage(damage, projectileStats.DamageType);
                AddDebuff(enemy);
                
                animator.SetTrigger("penetration");
            }
        }
    }
}
