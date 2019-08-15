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

    public override string GetTowerInfo()
    {
        string debuffInfo = string.Format("Change {0}% DOT enemy by {1} sec with DPS {2}", 
            rocketDebuff.ProcChance, 
            rocketDebuff.Duration,
            rocketDebuff.DamagePerSecond);
        
        
        string info = string.Format("<color=#ef5350>{0}</color>\nrange <color=#ef5350>{1}</color>\ndamage <color=#ef5350>{2}</color>\ndebuff\n{3}", 
            GetType(), 
            towerStats.Range, 
            towerStats.Damage,
            debuffInfo);

        return info;
    }
}
