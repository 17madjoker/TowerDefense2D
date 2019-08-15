using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] private CanonDebuff canonDebuff;
    
    public override Debuff CreateDebuff(Enemy target)
    {
        return new CanonDebuff(
            canonDebuff.Duration,
            canonDebuff.ProcChance,
            target,
            towerStats.DamageType);
    }

    public override int GetChance()
    {
        return canonDebuff.ProcChance;
    }

    public override string GetTowerInfo()
    {
        string debuffInfo = string.Format("Change {0}% to stun enemy by {1} sec", 
            canonDebuff.ProcChance, 
            canonDebuff.Duration);
        
        
        string info = string.Format("<color=#ef5350>{0}</color>\nrange <color=#ef5350>{1}</color>\ndamage <color=#ef5350>{2}</color>\ndebuff\n{3}", 
            GetType(), 
            towerStats.Range, 
            towerStats.Damage,
            debuffInfo);

        return info;
    }
}
