using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RocketDebuff : Debuff
{
    [SerializeField] private float damagePerSecond;
    private float timer = 0;

    public float DamagePerSecond { get { return damagePerSecond; } }

    public RocketDebuff(float duration, int procChance, float damagePerSecond, Enemy target, GameManager.typeOfDamage debuffType) : base(duration, procChance, target, debuffType)
    {
        this.damagePerSecond = damagePerSecond;
    }
    
    public override void ApplyDebuff()
    {       
        timer += Time.deltaTime;
        
        if (timer >= 1f)
        {
            timer = 0;
            
            target.DamageHealth(damagePerSecond);
            Debug.Log("2222");
        }
        
        target.GetComponent<SpriteRenderer>().color = new Color32(	239, 83, 80, 255);
        
        base.ApplyDebuff();
    }

    public override void RemoveDebuff()
    {
        target.GetComponent<SpriteRenderer>().color = Color.white;
        
        base.RemoveDebuff();
    }


}
