using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : Tower
{
    [SerializeField] private BulletDebuff bulletDebuff;
    
    public override Debuff CreateDebuff(Enemy target)
    {
        return new BulletDebuff(
            bulletDebuff.MultiplierShieldRecoveryDelay,
            bulletDebuff.Duration,
            bulletDebuff.ProcChance,
            target,
            towerStats.DamageType);
    }

    public override int GetChance()
    {
        return bulletDebuff.ProcChance;
    }
    
    public override string GetTowerInfo()
    {
        string debuffInfo = string.Format("Change {0}% to increase {1} times shiled reset on {2} sec", 
            bulletDebuff.ProcChance, 
            bulletDebuff.MultiplierShieldRecoveryDelay,
            bulletDebuff.Duration);
        
        
        string info = string.Format("<color=#ef5350>{0}</color>\nrange <color=#ef5350>{1}</color>\ndamage <color=#ef5350>{2}</color>\ndebuff\n{3}", 
            GetType(), 
            towerStats.Range, 
            towerStats.Damage,
            debuffInfo);

        return info;
    }
}
