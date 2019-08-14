using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject canvas;
    
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
            {
                GameOver();
            }
            
            baseHealthText.text = "Base health:\n <color=#ef5350>" + baseHealth + "</color>";
        }
    }

    private Text baseHealthText;
    private Text moneyText;
    private float timerIncome;

    private bool isGameOver = false;
    private bool isPaused = false;

    private void Start()
    {
        baseHealthText = GameObject.Find("BaseHealth").GetComponent<Text>();
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        
        Money = money;
        BaseHealth = baseHealth;
    }

    private void Update()
    {
        StartCoroutine(MoneyIncome(incomePerSecond));
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
