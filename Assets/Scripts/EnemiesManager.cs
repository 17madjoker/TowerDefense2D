using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesManager : MonoBehaviour
{    
    [SerializeField] private Wave[] waves;
    private int enemyId = 0;
    private int waveIndex = 0;
    private Text timeToNextWave;
    private Text remainedWaves;
    private GameObject towersPanel;
    private GameObject spawnButton;

    private Transform enemiesParent;
    private GameManager gameManager;
    
    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
        timeToNextWave = GameObject.Find("TimeToNextWave").GetComponent<Text>();
        remainedWaves = GameObject.Find("RemainedWaves").GetComponent<Text>();
        towersPanel = GameObject.Find("TowersPanel");
        spawnButton = GameObject.Find("SpawnButton");
        gameManager = FindObjectOfType<GameManager>();
        
        CheckRemainedWaves(waves);
    }

    private void Update()
    {
        SpawnWave();
    }

    private void SpawnWave()
    {
        if (waves.Length != 0)
        {
            if (waves[waveIndex].IsWaveEnded == false)
            {
                if (waves[waveIndex].TimeToStartWave > 0f)
                {
                    if (enemiesParent.childCount == 0)
                    {
                        timeToNextWave.text = "Time to next wave: \n" + "<color=#FFA726>" + Mathf.Round(waves[waveIndex].TimeToStartWave) + "</color>";
                        
                        spawnButton.GetComponent<Button>().interactable = true;
                        spawnButton.transform.GetChild(0).GetComponent<Text>().text =
                            "Spawn now and claim \n" + Mathf.Round(waves[waveIndex].TimeToStartWave) + " $";
                        
                        if (Time.timeScale != 1)
                            gameManager.SetGameSpeed(1);
                        
                        waves[waveIndex].TimeToStartWave -= Time.deltaTime;
                        
                        if (!gameManager.IsPaused)
                            towersPanel.SetActive(true);
                    }

                    else
                    {
                        timeToNextWave.text = "Defeat enemies";
                        spawnButton.transform.GetChild(0).GetComponent<Text>().text = "Spawn now";
                        spawnButton.GetComponent<Button>().interactable = false;
                    }
                }

                if (waves[waveIndex].TimeToStartWave <= 0f && enemiesParent.childCount == 0)
                {
                    StartCoroutine(SpawnEnemy(waves[waveIndex], waves[waveIndex].TimeBetweenEnemy));
                    
                    if (!gameManager.IsPaused)
                        towersPanel.SetActive(false);
                    
                    waves[waveIndex].IsWaveEnded = true;
                    CheckRemainedWaves(waves);

                    if (waveIndex == waves.Length - 1)
                        timeToNextWave.text = "<color=#ef5350>Final wave</color>";
                    
                    if (waveIndex < waves.Length - 1)
                        waveIndex++;
                }
            }
            
            else if (enemiesParent.childCount == 0 && waves[waveIndex].IsWaveEnded && waveIndex == waves.Length - 1)
                gameManager.LevelComplete();
        }
    }

    private IEnumerator SpawnEnemy(Wave wave, float timeBetweenEnemy)
    {
        for (int j = 0; j < wave.Enemies.Length; j++)
        {
            for (int i = 0; i < wave.Enemies[j].EnemyCount; i++)
            {
                GameObject tmp = Instantiate(wave.Enemies[j].EnemyPrefab);
                tmp.transform.SetParent(enemiesParent);
                
                tmp.GetComponent<Enemy>().SetEnemyId(enemyId);
                enemyId++;
            
                yield return new WaitForSeconds(timeBetweenEnemy);
            }
        }
    }

    private void CheckRemainedWaves(Wave[] waves)
    {
        int wavesRemained = waves.Length;

        for (int i = 0; i < waves.Length; i++)
        {
            if (waves[i].IsWaveEnded)
                wavesRemained--;
        }
        
        remainedWaves.text = "Remained waves:\n" + " <color=#FFA726>" + wavesRemained + "</color>";
    }

    public void NextWave()
    {
        if (waveIndex > 0 && waves[waveIndex - 1].IsWaveEnded && enemiesParent.childCount == 0 || waveIndex == 0)
        {
            gameManager.Money += (int) Mathf.Round(waves[waveIndex].TimeToStartWave);
            waves[waveIndex].TimeToStartWave = 0;
        }
    }

    public int GetTimeToStartWave()
    {
        return (int) Mathf.Round(waves[waveIndex].TimeToStartWave);
    }
}
