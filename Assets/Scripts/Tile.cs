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

    public void SetTile(Node currentNode, Vector3 tilePosition, bool isWayTile = false)
    {            
        Node = currentNode;
        nodeX = currentNode.x;
        nodeY = currentNode.y;
        transform.position = tilePosition;
        transform.SetParent(GameObject.Find("Tiles").transform);

        IsWayTile = isWayTile;
        wayTile = isWayTile;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        TowerManager TowerManager = GameObject.Find("TowerManager").GetComponent<TowerManager>();
        
        if (!EventSystem.current.IsPointerOverGameObject() && TowerManager.SelectedTower != null)
        {
            Instantiate(TowerManager.SelectedTower.TowerPrefab, transform.position, Quaternion.identity);
        }
    }
}
