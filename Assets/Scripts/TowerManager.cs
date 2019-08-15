using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject towerRange;
    [SerializeField] private GameObject sellTowerButton;
    [SerializeField] private GameObject towerInfo;
    [SerializeField] private Text towerInfoText;
    private GameObject followTower;
    private TowerButton selectedTower;
    private Tower tempTower;
    private Text messageField;
    private Tile pickedTile;

    public TowerButton SelectedTower
    {
        private set { selectedTower = value; }
        get { return selectedTower; }
    }
    
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
        if (followTower == null)
        {
            followTower = new GameObject();
            followTower.name = "MouseFollowTower";
        
            followTower.AddComponent<SpriteRenderer>();
            followTower.GetComponent<SpriteRenderer>().sprite =
                SelectedTower.TowerPrefab.GetComponent<SpriteRenderer>().sprite;
            followTower.GetComponent<SpriteRenderer>().sortingOrder = 1;

            GameObject range = Instantiate(towerRange);
            range.transform.localScale = new Vector3(SelectedTower.TowerRange, SelectedTower.TowerRange, 1);
            range.transform.SetParent(followTower.transform);
      
            messageField.text = "Press <color=#FFA726><ESC></color> cancel tower selection";
        }
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

    public void SellTower()
    {
        if (tempTower != null)
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            tempTower.Tile.IsEmptyTile = true;
            gameManager.Money += tempTower.GetTowerPrice() / 2;
            
            Destroy(tempTower.gameObject);
            HideTowerInfo();
        }
    }

    public void ShowTowerInfo(Tower selectedTower)
    {
        if (tempTower != null)
        {
            TowerInfo(selectedTower);
            tempTower.Select();
        }

            
        tempTower = selectedTower;
        tempTower.Select();
        
        sellTowerButton.SetActive(true);
        TowerInfo(selectedTower);
        sellTowerButton.transform.GetChild(0).GetComponent<Text>().text = "Sell if for " + "<color=white>" + selectedTower.GetTowerPrice() / 2 + " $</color>";
    }

    public void HideTowerInfo()
    {
        if (tempTower != null)
        {
            towerInfo.active = !towerInfo.active;
            tempTower.Select();
        }

        
        sellTowerButton.SetActive(false);
        tempTower = null;
    }

    public void TowerInfo(Tower tower)
    {
        towerInfo.active = !towerInfo.active;

        string info = tower.GetTowerInfo();

        towerInfoText.text = info;
    }
}

