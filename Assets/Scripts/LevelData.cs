using System;
using System.Collections.Generic;

[Serializable]
public struct LevelData
{
    private int level;
    private bool isComplete;
    private Dictionary<starCondition, bool> levelStars;
    
    private enum starCondition
    {
        WinHundredMoreMoney,
        WinAtLeastOneMaxTower,
        WinWithMaxBaseHealth
    }

    public int Level { get { return level; } }

    public LevelData(int level)
    {
        this.level = level;
        isComplete = false;
        
        levelStars = new Dictionary<starCondition, bool>();
        levelStars.Add(starCondition.WinHundredMoreMoney, false);
        levelStars.Add(starCondition.WinAtLeastOneMaxTower, false);
        levelStars.Add(starCondition.WinWithMaxBaseHealth, false);
    }
}
