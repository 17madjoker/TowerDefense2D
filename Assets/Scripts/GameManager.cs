using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int money;

    public int Money
    {
        get { return money; }
        set
        {
            money = value;
            moneyText.text = "Money:\n" + money + " <color=#FFA726>$</color>";
        }
    }

    private Text moneyText;
    private EnemiesManager EnemiesManager;
    private int incomePerSecond;
    private float timeToIncome = 2f;
    
    private void Start()
    {
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        EnemiesManager = GameObject.Find("EnemiesManager").GetComponent<EnemiesManager>();
        
        Money = 300;
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
}
