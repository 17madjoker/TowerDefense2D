using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private float speed;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
        
            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotate = Quaternion.AngleAxis(angel, Vector3.forward);
            rotate *= Quaternion.Euler(0, 0, -90);
        
            transform.rotation = rotate;
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }

    public void SetProjectile(Tower parentTower)
    {
        target = parentTower.EnemyTarget;
        
        transform.position = parentTower.transform.position;
        transform.SetParent(parentTower.transform);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
