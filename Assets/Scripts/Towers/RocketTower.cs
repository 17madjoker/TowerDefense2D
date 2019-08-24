using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTower : Tower
{
    [SerializeField] private RocketDebuff rocketDebuff;

    [SerializeField] private RocketTowerUpgrade[] upgrades;
    private int upgradeIndex = 0;
    private bool isMaxLevel = false;

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

    public override string GetTowerInfo(bool isUpgradeInfo)
    {
        string procChanceIncrease = string.Empty;
        string debuffDurationIncrease = string.Empty;
        string damagePerSecondIncrease = string.Empty;
        string rangeIncrease = string.Empty;
        string damageIncrease = string.Empty;

        if (isUpgradeInfo)
        {
            RocketTowerUpgrade upgrade = upgrades[0];

            procChanceIncrease = " + " + upgrade.ProcChanceIncrease;
            debuffDurationIncrease = " + " + upgrade.DebuffDurationIncrease;
            damagePerSecondIncrease = " + " + upgrade.DamagePerSecondIncrease;
            rangeIncrease = " + " + upgrade.RangeIncrease;
            damageIncrease = " + " + upgrade.DamageIncrease;
        }
        
        string debuffInfo = string.Format("Change <color=#ef5350>{0}</color>% DOT enemy by <color=#ef5350>{1}</color> sec with DPS <color=#ef5350>{2}</color>", 
            rocketDebuff.ProcChance + " <color=#aed581>" + procChanceIncrease + "</color> ", 
            rocketDebuff.Duration + " <color=#aed581>" + debuffDurationIncrease + "</color>",
            rocketDebuff.DamagePerSecond + " <color=#aed581>" + damagePerSecondIncrease + "</color>");
        
        
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
            RocketTowerUpgrade upgrade = upgrades[UpgradeIndex];
        
            towerStats.Level = upgrade.Level;
            towerStats.Price += upgrade.UpgradePrice;
            towerStats.Damage += upgrade.DamageIncrease;
            towerStats.Range += upgrade.RangeIncrease;
            rocketDebuff.Duration += upgrade.DebuffDurationIncrease;
            rocketDebuff.ProcChance += upgrade.ProcChanceIncrease;
            rocketDebuff.DamagePerSecond += upgrade.DamagePerSecondIncrease;

            GameObject.Find("GameManager").GetComponent<GameManager>().Money -= upgrade.UpgradePrice;
            
            UpgradeIndex++;
        }
        
        if (UpgradeIndex == upgrades.Length)
            IsMaxLevel = true;
    }
}
