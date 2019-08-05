using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour
{
    private GameObject followTower;
    private TowerButton selectedTower;

    public TowerButton SelectedTower
    {
        private set { selectedTower = value; }
        get { return selectedTower; }
    }

    private Text messageField;
    
    private void Start()
    {
        messageField = GameObject.Find("MessageField").GetComponent<Text>();
    }

    private void Update()
    {
        MouseFollowTower();
    }

    public void SelectTower(TowerButton selectedTower)
    {
        if (isEnoughMoney(selectedTower))
        {
            SelectedTower = selectedTower;
            CreateFollowTower();
        }
    }

    private void MouseFollowTower()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            DestroyFollowTower();
        }
        else if (followTower != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            followTower.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
        }
    }

    private void CreateFollowTower()
    {
        followTower = new GameObject();
        followTower.name = "MouseFollowTower";
        
        followTower.AddComponent<SpriteRenderer>();
        followTower.GetComponent<SpriteRenderer>().sprite =
            SelectedTower.TowerPrefab.GetComponent<SpriteRenderer>().sprite;
        followTower.GetComponent<SpriteRenderer>().sortingOrder = 1;

        messageField.text = "Press <color=#FFA726><ESC></color> cancel tower selection";
    }

    public void DestroyFollowTower()
    {
        Destroy(followTower.gameObject);
        SelectedTower = null;
            
        messageField.text = "";
    }

    private bool isEnoughMoney(TowerButton selectedTower)
    {
        int currentMoney = GameObject.Find("GameManager").GetComponent<GameManager>().Money;

        if (currentMoney >= selectedTower.TowerPrice)
            return true;

        return false;
    }

    public bool BuyTower(TowerButton purchasedTower)
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gameManager.Money >= purchasedTower.TowerPrice)
        {
            gameManager.Money -= purchasedTower.TowerPrice;
            return true;
        }

        return false;
    }
}

