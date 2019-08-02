using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject bulletTowerPref;
    [SerializeField] 
    private GameObject rocketTowerPref;
    [SerializeField] 
    private GameObject canonTowerPref;

    public GameObject BulletTowerPref { get { return bulletTowerPref; } }
    public GameObject RocketTowerPref { get { return rocketTowerPref; } }
    public GameObject CanonTowerPref { get { return canonTowerPref; } }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}
