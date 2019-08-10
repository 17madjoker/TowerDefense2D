using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Text priceText;
    private int towerPrice;
    private float towerRange;
    
    public GameObject TowerPrefab { get { return towerPrefab; } }

    public int TowerPrice
    {
        get { return towerPrice; }
        set
        {
            towerPrice = value;
            priceText.text = towerPrice + " <color=#FFA726>$</color>";
        }
    }
    
    public float TowerRange
    {
        get { return towerRange; }
        private set { towerRange = value; }
    }

    private void Start()
    {
        priceText.text = towerPrice + " <color=#FFA726>$</color>";

        TowerPrice = towerPrefab.GetComponent<Tower>().GetTowerPrice();
        TowerRange = towerPrefab.GetComponent<Tower>().GetTowerRange();
    }
}
