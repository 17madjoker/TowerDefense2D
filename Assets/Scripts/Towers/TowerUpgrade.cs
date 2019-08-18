using UnityEngine;

public abstract class TowerUpgrade
{
    [SerializeField] protected int level;
    [SerializeField] protected int upgradePrice;
    [SerializeField] protected int damageIncrease;
    [SerializeField] protected float rangeIncrease;
    [SerializeField] protected float debuffDurationIncrease;
    [SerializeField] protected int procChanceIncrease;
    
    public int Level { get { return level; } }
    public int UpgradePrice { get { return upgradePrice; } }
    public int DamageIncrease {get { return damageIncrease; } }
    public float RangeIncrease { get { return rangeIncrease; } }
    public float DebuffDurationIncrease { get { return debuffDurationIncrease; } }
    public int ProcChanceIncrease { get { return procChanceIncrease; } }
}
