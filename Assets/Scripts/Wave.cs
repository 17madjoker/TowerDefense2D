using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public float TimeToStartWave { get; set; }
    public float TimeBetweenEnemy { get; private set; }

    public bool isWaveEnded;
    public Dictionary<GameObject, int> Enemies { get; private set; }
    
    public Wave(float timeToStartWave, float timeBetweenEnemy)
    {
        Enemies = new Dictionary<GameObject, int>();
        
        TimeToStartWave = timeToStartWave;
        TimeBetweenEnemy = timeBetweenEnemy;
        isWaveEnded = false;
    }
}
