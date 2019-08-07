using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int money;
    private int baseHealth;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject canvas;

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
            {
                GameOver();
            }
            
            baseHealthText.text = "Base health:\n <color=#ef5350>" + baseHealth + "</color>";
        }
    }

    private Text baseHealthText;
    private Text moneyText;
    private EnemiesManager EnemiesManager;
    private int incomePerSecond;
    private float timeToIncome = 2f;

    private bool isGameOver = false;
    private bool isPaused = false;
    
    private void Start()
    {
        baseHealthText = GameObject.Find("BaseHealth").GetComponent<Text>();
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        EnemiesManager = GameObject.Find("EnemiesManager").GetComponent<EnemiesManager>();
        
        Money = 300;
        BaseHealth = 9;
        incomePerSecond = 2;
    }

    private void Update()
    {
        StartCoroutine(MoneyIncome(incomePerSecond));
    }

    private IEnumerator MoneyIncome(int incomePerSecond)
    {
        if (timeToIncome <= 0f)
        {
            Money += incomePerSecond;
            timeToIncome = 2f;
            
            yield return new WaitForSeconds(0);
        }

        timeToIncome -= Time.deltaTime;
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
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;
            HideShowUI();

            Time.timeScale = 0;
            gameMenu.SetActive(true);
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

    private void HideShowUI()
    {
        for (int i = 0; i < canvas.transform.GetChildCount(); i++)
        {
            GameObject UIobj = canvas.transform.GetChild(i).gameObject;
            
            if (UIobj.name != gameMenu.name)
            {
                if (isPaused || isGameOver)
                    UIobj.SetActive(false);
                    
                else if (!isPaused)
                    UIobj.SetActive(true);
            }
        }
    }
}
