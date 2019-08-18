using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class FloatingDamage : MonoBehaviour
{
    private float damage;
    private Transform enemyTransform;
    private float hideSpeed;
    private float offsetX;

    public float Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public Transform EnemyTransform
    {
        get { return enemyTransform; }
        set { enemyTransform = value; }
    }

    public float HideSpeed
    {
        get { return hideSpeed; }
        set { hideSpeed = value; }
    }

    public string DamageType { get; set; }

    private void Start()
    {
        GetComponent<Animator>().SetTrigger(DamageType);
        GetComponent<Text>().text = Mathf.Round(damage).ToString();
        offsetX = Random.Range(-0.3f, 0.3f);
        
        Destroy(transform.parent.gameObject, HideSpeed);
    }

    private void Update()
    {
        if (transform.parent != null && enemyTransform != null)
        {
            transform.parent.position = enemyTransform.position;
            transform.parent.position += Vector3.Lerp(transform.parent.position,new Vector3(offsetX, 0, 0), HideSpeed);
        }
    }

}
