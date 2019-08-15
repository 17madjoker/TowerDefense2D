using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BulletDebuff : Debuff
{
    // increase time to shield recovery time by multiplier on some duration
    
    [SerializeField] private float multiplierShieldRecovery;
    private float defaultRecoveryTime;

    public float MultiplierShieldRecoveryDelay { get { return multiplierShieldRecovery; } }
    
    public BulletDebuff(float multiplier, float duration, int procChance, Enemy target, GameManager.typeOfDamage debuffType) : base(duration, procChance, target, debuffType)
    {
        multiplierShieldRecovery = multiplier;
        defaultRecoveryTime = target.GetTimeToShieldRecovery();
    }

    public override void ApplyDebuff()
    {
        target.SetTimeToShieldRecovery(defaultRecoveryTime * multiplierShieldRecovery);
        
        base.ApplyDebuff();
    }
    
    public override void RemoveDebuff()
    {
        target.SetTimeToShieldRecovery(defaultRecoveryTime);
        
        base.RemoveDebuff();
    }
}
