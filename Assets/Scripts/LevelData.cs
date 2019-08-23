using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    private int level;
    private bool isComplete;
    private Dictionary<starCondition, bool> levelStars;
    
    public enum starCondition
    {
        WinHundredMoreMoney,
        WinAtLeastOneMaxTower,
        WinWithMaxBaseHealth
    }

    public int Level
    {
        set { level = value; }
        get { return level; }
    }

    public bool IsComplete
    {
        set { isComplete = value; }
        get { return isComplete; }
    }
    
    public Dictionary<starCondition, bool> LevelStars
    {
        get { return levelStars; }
        set { levelStars = value; }
    }

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
