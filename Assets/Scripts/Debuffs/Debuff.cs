using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff
{
    [SerializeField] private float duration;
    [SerializeField] private int procChance;
    private GameManager.typeOfDamage type;
    private float durationTimer = 0f;
    protected Enemy target;

    public int ProcChance { get { return procChance; } }
    
    public float Duration { get { return duration; } }

    public GameManager.typeOfDamage Type { get { return type; } }

    public float DurationTimer 
    {
        get { return durationTimer; }
        set { durationTimer = value; }
    }

    public Debuff(float duration, int procChance, Enemy target, GameManager.typeOfDamage debuffType)
    {
        this.duration = duration;
        this.procChance = procChance;
        this.target = target;
        type = debuffType;
    }

    public virtual void ApplyDebuff()
    {
        if (durationTimer >= duration)
        {
            RemoveDebuff();
            durationTimer = 0;
        }


        durationTimer += Time.deltaTime;
    }

    public virtual void RemoveDebuff()
    {
        target.RemoveDebuff(this);
    }
}
