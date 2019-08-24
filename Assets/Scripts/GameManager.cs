using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(2)]
public class GameManager : MonoBehaviour
{
    
    [SerializeField] private int money;
    [SerializeField] private int baseHealth;
    [SerializeField] private int incomePerSecond;
    [SerializeField] private float timeToIncome;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private Text gameMenuText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text gameSpeedText;
    
    private Text baseHealthText;
    private Text moneyText;
    private float timerIncome;
    private TowerManager towerManager;

    private bool isGameOver = false;
    private bool isPaused = false;
    private bool isComplete = false;
    private List<GameObject> activeCanvasElements = new List<GameObject>();
    private int MaxBaseHealth;

    public bool IsPaused { get { return isPaused; } }

    public enum typeOfDamage { Bullet, Canon, Rocket }

    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            moneyText.text = "Money:\n" + money + " <color=#FFA726>$</color>";
        }
    }
    
    public int BaseHealth
    {
        get { return baseHealth; }
        set
        {
            baseHealth = value;

            if (baseHealth == 0)
                GameOver();
            
            baseHealthText.text = "Base health:\n <color=#ef5350>" + baseHealth + "</color>";
        }
    }

    private void Start()
    {
        CreateActiveCanvasElements();
        
        towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        baseHealthText = GameObject.Find("BaseHealth").GetComponent<Text>();
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        
        Money = money;
        BaseHealth = baseHealth;
        MaxBaseHealth = BaseHealth;
    }

    private void Update()
    {
//        StartCoroutine(MoneyIncome(incomePerSecond));
    }

    private IEnumerator MoneyIncome(int incomePerSecond)
    {
        if (timerIncome <= 0f)
        {
            Money += incomePerSecond;
            timerIncome = timeToIncome;
            
            yield return new WaitForSeconds(0);
        }

        timerIncome -= Time.deltaTime;
    }

    public void SetGameSpeed(int gameSpeed)
    {
        if (gameSpeed == 1)
        {
            gameSpeedText.text = "Game Speed x1";
            Time.timeScale = 1;
        }
        
        else if (Time.timeScale == 1)
        {
            gameSpeedText.text = "Game Speed x2";
            Time.timeScale = 2;
        }
        
        else if (Time.timeScale == 2)
        {
            gameSpeedText.text = "Game Speed x3";
            Time.timeScale = 3;
        }
        
        else if (Time.timeScale == 3)
        {
            gameSpeedText.text = "Game Speed x1";
            Time.timeScale = 1;
        }
    }

    private void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            HideShowUI();

            Time.timeScale = 0;
            gameMenu.SetActive(true);
            
            GameObject.Find("Resume").SetActive(false);
            gameMenuText.text = "Game Over";
        }
    }

    public void LevelComplete()
    {
        int levelNum = Convert.ToInt32(SceneManager.GetActiveScene().name.Split('_').Last());
        
        LevelData levelData = new LevelData(levelNum);

        if (Money >= 100)
            levelData.LevelStars[LevelData.starCondition.WinHundredMoreMoney] = true;
        
        if (TowerManager.IsTowerOfMaxLevel())
            levelData.LevelStars[LevelData.starCondition.WinAtLeastOneMaxTower] = true;
        
        if (MaxBaseHealth == BaseHealth)
            levelData.LevelStars[LevelData.starCondition.WinWithMaxBaseHealth] = true;

        levelData.IsComplete = true;
        isComplete = true;
        
        SaveLoadManager.SaveLevel(SceneManager.GetActiveScene().name, levelData);
        
        Pause();
    }

    public void Pause()
    {
        if (!isPaused)
        {
            towerManager.DisableTowerInfo();
            
            isPaused = true;
            HideShowUI();

            Time.timeScale = 0;
            gameMenu.SetActive(true);

            if (isComplete)
            {
                gameMenuText.text = "Level complete";
                
                GameObject.Find("Resume").SetActive(false);
            }
        }
    }

    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            HideShowUI();

            Time.timeScale = 1;
            gameMenu.SetActive(false);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene(sceneName);
    }

    private void CreateActiveCanvasElements()
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            GameObject UIobj = canvas.transform.GetChild(i).gameObject;
            
            if (UIobj.activeInHierarchy)
                activeCanvasElements.Add(UIobj);
        }
    }

    private void HideShowUI()
    {
        for (int i = 0; i < activeCanvasElements.Count; i++)
        {
            if (isPaused || isGameOver)
                activeCanvasElements[i].SetActive(false);
                    
            else if (!isPaused)
                activeCanvasElements[i].SetActive(true);
        }
    }
}
