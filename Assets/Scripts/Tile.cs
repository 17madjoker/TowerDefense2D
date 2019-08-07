using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Node Node { get; private set; }
    
    // For test
    public int nodeX;
    public int nodeY;
    public bool wayTile;
    
    public bool IsWayTile { get; set; }
    public bool IsEmptyTile { get; private set; }
    
    private SpriteRenderer spriteRenderer;
    private Color32 red = new Color32(239, 83, 80, 255);
    private Color32 green = new Color32(102, 187, 106, 255);

    private TowerManager towerManager;
    private Tower tower;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        towerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
    }

    public void SetTile(Node currentNode, Vector3 tilePosition, bool isWayTile = false, bool isEmptyTile = false)
    {            
        Node = currentNode;
        nodeX = currentNode.x;
        nodeY = currentNode.y;
        transform.position = tilePosition;
        transform.SetParent(GameObject.Find("Tiles").transform);

        IsEmptyTile = isEmptyTile;
        IsWayTile = isWayTile;
        wayTile = isWayTile;
    }

    private void OnMouseOver()
    {
        PlaceTower();
        TowerInfo(tower);
    }

    private void OnMouseExit()
    {
        spriteRenderer.color = Color.white;
    }

    private void PlaceTower()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerManager.SelectedTower != null)
        {
            ColorHoverTile();
            
            if (Input.GetMouseButtonDown(0) && IsEmptyTile)
            {
                if (towerManager.BuyTower(towerManager.SelectedTower))
                {
                    Transform towersParent = GameObject.Find("Towers").transform;
                    GameObject currentTower = Instantiate(towerManager.SelectedTower.TowerPrefab, transform.position, Quaternion.identity);
                    currentTower.transform.SetParent(towersParent);

                    IsEmptyTile = false;
                    tower = currentTower.GetComponent<Tower>();
                    spriteRenderer.color = Color.white;
            
                    towerManager.DestroyFollowTower(); 
                }
            }
        }
    }

    private void TowerInfo(Tower selectedTower)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerManager.SelectedTower == null && Input.GetMouseButtonDown(0))
        {
            if (tower != null)
            {
                towerManager.ShowTowerInfo(selectedTower);
                Debug.Log("if");
            }
            else
            {
                towerManager.HideTowerInfo();
                Debug.Log("Else");
            }
        }
    }

    private void ColorHoverTile()
    {
        if (IsEmptyTile && !wayTile)
            spriteRenderer.color = green;
        
        else if (!IsEmptyTile && !wayTile)
            spriteRenderer.color = red;
    }
}
