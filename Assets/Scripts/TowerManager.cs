using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private TowerButton selectedTower;

    public TowerButton SelectedTower
    {
        private set { selectedTower = value; }
        get { return selectedTower; }
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void SelectTower(TowerButton selectedTower)
    {
        SelectedTower = selectedTower;
    }
}
