using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(4)]
public class Enemy : MonoBehaviour
{
    private List<Transform> wayPoints;
    private int wayPointIndex = 0;
    private Animator animator;
    private bool isDead = false;

    public bool IsDead { get { return isDead; } }

    [SerializeField] private EnemyStats enemyStats;

    private void Awake()
    {
        enemyStats.Init();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        wayPoints = GameObject.Find("MapManager").GetComponent<MapManager>().WayPoints;

        SetStartPosition();
    }

    private void Update()
    {
        if (!isDead)
        {
            CheckHealth();
            Move();
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

    public void TakeDamage(float damage)
    {
        enemyStats.CurrentHealth -= damage;
    }

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

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        if (!GameObject.Find("Enemies").transform.GetChild(0))
//            speed = 0;
//    }
//    
//    private void OnTriggerExit2D(Collider2D other)
//    {
//        speed = other.gameObject.GetComponent<Enemy>().speed;
//    }
}
