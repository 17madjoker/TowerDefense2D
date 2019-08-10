using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
//    public float TimeToStartWave { get; set; }
//    public float TimeBetweenEnemy { get; private set; }
//
//    public bool isWaveEnded;
//    
//    public Dictionary<GameObject, int> Enemies { get; private set; }
//    
//    public Wave(float timeToStartWave, float timeBetweenEnemy)
//    {
//        Enemies = new Dictionary<GameObject, int>();
//        
//        TimeToStartWave = timeToStartWave;
//        TimeBetweenEnemy = timeBetweenEnemy;
//        isWaveEnded = false;
//    }

    [SerializeField] private float timeToStartWave;
    [SerializeField] private float timeBetweenEnemy;
    [SerializeField] private enemyStack[] enemies;
    private bool isWaveEnded = false;

    public bool IsWaveEnded
    {
        get { return isWaveEnded; }
        set { isWaveEnded = value; }
    }

    public float TimeToStartWave
    {
        get { return timeToStartWave; }
        set { timeToStartWave = value; }
    }

    public float TimeBetweenEnemy
    {
        get { return timeBetweenEnemy; }
        set { timeBetweenEnemy = value; }
    }
    
    public enemyStack[] Enemies { get { return enemies; } }
    
    [Serializable]
    public class enemyStack
    {
        [SerializeField] private int enemyCount;
        [SerializeField] private GameObject enemyPrefab;
        
        public int EnemyCount { get { return enemyCount; } }
        public GameObject EnemyPrefab { get { return enemyPrefab; } }
    }
}
