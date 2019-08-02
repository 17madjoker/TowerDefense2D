using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject aircraftPref;    
    [SerializeField]
    private GameObject soldiesPref;    
    [SerializeField]
    private GameObject tankPref;

    private List<Wave> waves;
    private int waveIndex = 0;
    private Text timeToNextWave;
    
    private void Start()
    {
        timeToNextWave = GameObject.Find("TimeToNextWave").GetComponent<Text>();
        
        waves = new List<Wave>();
        
        waves.Add(new Wave(3f, 1.5f));
        waves[0].Enemies.Add(aircraftPref, 3);
        waves[0].Enemies.Add(tankPref, 2);
        
        waves.Add(new Wave(15f, 1f));
        waves[1].Enemies.Add(soldiesPref, 3);

    }

    private void Update()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        if (waves.Count != 0)
        {
            if (waves[waveIndex].isWaveEnded == false)
            {
                if (waves[waveIndex].TimeToStartWave > 0f)
                {
                    timeToNextWave.text = "Time to next wave: \n" + "<color=#FFA726>" + Mathf.Round(waves[waveIndex].TimeToStartWave) + "</color>";
                    waves[waveIndex].TimeToStartWave -= Time.deltaTime;
                }

                if (waves[waveIndex].TimeToStartWave <= 0f)
                {
                    StartCoroutine(SpawnEnemy(waves[waveIndex].Enemies, waves[waveIndex].TimeBetweenEnemy));
                    waves[waveIndex].isWaveEnded = true;

                    if (waveIndex < waves.Count - 1)
                        waveIndex++;
                    
                    if (waveIndex == waves.Count - 1)
                        timeToNextWave.text = "<color=#FFA726>Final wave</color>";
                }
            }
        }
    }

    private IEnumerator SpawnEnemy(Dictionary<GameObject, int> enemies, float timeBetweenEnemy)
    {
        foreach (KeyValuePair<GameObject, int> enemy in enemies)
        {
            for (int i = 0; i < enemy.Value; i++)
            {
                GameObject tmp = Instantiate(enemy.Key);
                tmp.transform.SetParent(transform);
            
                yield return new WaitForSeconds(timeBetweenEnemy);
            }
        }
    }

    public void NextWave()
    {
        if (waveIndex > 0 && waves[waveIndex - 1].isWaveEnded)
            waves[waveIndex].TimeToStartWave = 0;
    }
}
