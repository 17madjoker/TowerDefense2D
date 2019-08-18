using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTower : Tower
{
    [SerializeField] private BulletDebuff bulletDebuff;

    [SerializeField] private BulletTowerUpgrade[] upgrades;
    private int upgradeIndex = 0;
    private bool isMaxLevel = false;

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
    
    public override string GetTowerInfo(bool isUpgradeInfo)
    {
        string procChanceIncrease = string.Empty;
        string debuffDurationIncrease = string.Empty;
        string multiplierIncrease = string.Empty;
        string rangeIncrease = string.Empty;
        string damageIncrease = string.Empty;

        if (isUpgradeInfo)
        {
            BulletTowerUpgrade upgrade = upgrades[0];

            procChanceIncrease = " + " + upgrade.ProcChanceIncrease;
            debuffDurationIncrease = " + " + upgrade.DebuffDurationIncrease;
            multiplierIncrease = " + " + upgrade.MultiplierIncrease;
            rangeIncrease = " + " + upgrade.RangeIncrease;
            damageIncrease = " + " + upgrade.DamageIncrease;
        }
        
        string debuffInfo = string.Format("Change <color=#ef5350>{0}</color>% to increase <color=#ef5350>{1}</color> times shiled reset on <color=#ef5350>{2}</color> sec", 
            bulletDebuff.ProcChance + " <color=#aed581>" + procChanceIncrease + "</color> ", 
            bulletDebuff.MultiplierShieldRecoveryDelay + " <color=#aed581>" + multiplierIncrease + "</color>",
            bulletDebuff.Duration + " <color=#aed581>" + debuffDurationIncrease + "</color>");
        
        
        string info = string.Format("<color=#ef5350>{0}</color> level {1}\nrange <color=#ef5350>{2}</color>\ndamage <color=#ef5350>{3}</color>\ndebuff\n{4}", 
            GetType(), 
            towerStats.Level,
            towerStats.Range + " <color=#aed581>" + rangeIncrease + "</color>", 
            towerStats.Damage + " <color=#aed581>" + damageIncrease + "</color>",
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
            BulletTowerUpgrade upgrade = upgrades[UpgradeIndex];
        
            towerStats.Level = upgrade.Level;
            towerStats.Price += upgrade.UpgradePrice;
            towerStats.Damage += upgrade.DamageIncrease;
            towerStats.Range += upgrade.RangeIncrease;
            bulletDebuff.Duration += upgrade.DebuffDurationIncrease;
            bulletDebuff.ProcChance += upgrade.ProcChanceIncrease;
            bulletDebuff.MultiplierShieldRecoveryDelay += upgrade.MultiplierIncrease;

            UpgradeIndex++;
        }
        
        if (UpgradeIndex == upgrades.Length)
            isMaxLevel = true;
    }
}
