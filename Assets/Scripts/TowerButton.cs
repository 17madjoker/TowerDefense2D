using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private Text priceText;
    [SerializeField] private int towerPrice;
    
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

    private void Start()
    {
        priceText.text = towerPrice + " <color=#FFA726>$</color>";
    }
}
