using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonTower : Tower
{
    [SerializeField] private CanonDebuff canonDebuff;

    [SerializeField] private CanonTowerUpgrade[] upgrades;
    private int upgradeIndex = 0;
    private bool isMaxLevel = false;
    
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

    public override string GetTowerInfo(bool isUpgradeInfo)
    {
        string procChanceIncrease = string.Empty;
        string debuffDurationIncrease = string.Empty;
        string rangeIncrease = string.Empty;
        string damageIncrease = string.Empty;

        if (isUpgradeInfo)
        {
            CanonTowerUpgrade upgrade = upgrades[0];

            procChanceIncrease = " + " + upgrade.ProcChanceIncrease;
            debuffDurationIncrease = " + " + upgrade.DebuffDurationIncrease;
            rangeIncrease = " + " + upgrade.RangeIncrease;
            damageIncrease = " + " + upgrade.DamageIncrease;
        }
        
        string debuffInfo = string.Format("Change <color=#ef5350>{0}</color>% to stun enemy by <color=#ef5350>{1}</color> sec", 
            canonDebuff.ProcChance + " <color=#aed581>" + procChanceIncrease + "</color> ", 
            canonDebuff.Duration + " <color=#aed581>" + debuffDurationIncrease + "</color> ");
        
        
        string info = string.Format("<color=#ef5350>{0}</color> level {1}\nrange <color=#ef5350>{2}</color>\ndamage <color=#ef5350>{3}</color>\ndebuff\n{4}", 
            GetType(), 
            towerStats.Level,
            towerStats.Range + " <color=#aed581>" + rangeIncrease + "</color> ", 
            towerStats.Damage + " <color=#aed581>" + damageIncrease + "</color> ",
            debuffInfo);

        return info;
    }

    public override TowerUpgrade GetUpgrade
    {
        get
        {
            if (UpgradeIndex <= upgrades.Length - 1)
                return upgrades[UpgradeIndex];

            return null;
        }
    }
    
    protected override int UpgradeIndex { get { return upgradeIndex; } set { upgradeIndex = value; } }

    public override bool IsMaxLevel { get { return isMaxLevel; } protected set { isMaxLevel = value; } }

    public override void Upgrade()
    {
        if (!IsMaxLevel)
        {
            CanonTowerUpgrade upgrade = upgrades[UpgradeIndex];
        
            towerStats.Level = upgrade.Level;
            towerStats.Price += upgrade.UpgradePrice;
            towerStats.Damage += upgrade.DamageIncrease;
            towerStats.Range += upgrade.RangeIncrease;
            canonDebuff.Duration += upgrade.DebuffDurationIncrease;
            canonDebuff.ProcChance += upgrade.ProcChanceIncrease;

            UpgradeIndex++;
        }
        
        if (UpgradeIndex == upgrades.Length)
            IsMaxLevel = true;
    }
}
