using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTower : Tower
{
    [SerializeField] private RocketDebuff rocketDebuff;
    
    public override Debuff CreateDebuff(Enemy target)
    {
        return new RocketDebuff(
            rocketDebuff.Duration,
            rocketDebuff.ProcChance,
            rocketDebuff.DamagePerSecond,
            target, 
            towerStats.DamageType);
    }

    public override int GetChance()
    {
        return rocketDebuff.ProcChance;
    }
}
