using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour
{
    private GameObject targetEnemy;
    [SerializeField] private SpriteRenderer spriteRange;
    [SerializeField] private int price;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private typeOfDamage damageType;
    
    public enum typeOfDamage
    { Bullet, Canon, Rocket }
    
    public int Price
    {
        get { return price; }
        private set { price = value; }
    }

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
    
    public int Damage
    {
        get { return damage; }
        private set { damage = value; }
    }
    
    public int AttackSpeed
    {
        get { return attackSpeed; }
        private set { attackSpeed = value; }
    }

    private void Start()
    {
        Range = range;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Enemy"))
        {
            Debug.Log(target.name);
        }
    }

    public void Select()
    {
        spriteRange.enabled = !spriteRange.enabled;
    }

//    public void SetTowerStats(int range, int damage, int attackSpeed, bool isTowerPlaced)
//    {
//        Range = range;
//        Damage = damage;
//        AttackSpeed = attackSpeed;
//    }
}
