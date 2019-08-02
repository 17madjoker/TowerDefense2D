using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(4)]
public class Enemy : MonoBehaviour
{
    private List<Transform> wayPoints;
    private int wayPointIndex = 0;
    
    private float speed = 1f;
    private float rotationSpeed = 10f;

    private EnemyType EnemyType;
    
    private void Start()
    {
        wayPoints = GameObject.Find("MapManager").GetComponent<MapManager>().WayPoints;

        SetStartPosition();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direction;
        float distanceToWaypoint;
        float angel;
        Quaternion rotate;
        
        direction = wayPoints[wayPointIndex].position - transform.position;
        distanceToWaypoint = Vector2.Distance(transform.position, wayPoints[wayPointIndex].position);
     
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].position, speed * Time.deltaTime);
        
        angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotate = Quaternion.AngleAxis(angel, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, rotationSpeed * Time.deltaTime);
        
        if (distanceToWaypoint <= 0.01f)
        {
            if (wayPointIndex < wayPoints.Count - 1)
            {
                wayPointIndex++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void SetStartPosition()
    {
        transform.position = wayPoints[0].position;
        
        Vector2 direction = wayPoints[1].position - transform.position;
        float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angel, Vector3.forward);
    }
}
