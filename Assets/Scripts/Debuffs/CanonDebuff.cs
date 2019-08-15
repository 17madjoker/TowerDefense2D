using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CanonDebuff : Debuff
{
    // stun for duration time

    public CanonDebuff(float duration, int procChance, Enemy target, GameManager.typeOfDamage debuffType) : base(duration, procChance, target, debuffType) {}
    
    public override void ApplyDebuff()
    {
        target.IsStop = true;
        target.IsStun = true;
        
        base.ApplyDebuff();
    }

    public override void RemoveDebuff()
    {
        target.IsStop = false;
        target.IsStun = false;
        
        base.RemoveDebuff();
    }
}
