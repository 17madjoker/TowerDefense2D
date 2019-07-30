using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;

    private string[] map;
    
    public static int MapWidth { get; private set; }
    public static int MapHeight { get; private set; }
    private int wayTilesCount = 0;

    private float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    public Tile StartWayTile { get; private set; }
    public Tile FinalWayTile { get; private set; }

    public Dictionary<Node, Tile> Tiles { get; private set; }  
    public Dictionary<Node, Tile> WayTiles { get; private set; }
    public List<GameObject> WayPoints { get; private set; }
     
    private void Start()
    {
        ParseLevelMap();
        CreateMap();
        PlaceCamera();

        CreateWayTilesList();
        CheckWayTilesList();
        Debug.Log(WayTiles.Count);
        CreateWaypoints();
    }
    
    private void ParseLevelMap()
    {
        TextAsset mapData = (TextAsset) Resources.Load("Level_1");

        string tmp = mapData.text.Replace(Environment.NewLine, String.Empty);
        
        map = tmp.Split('|');
    }
    
    private void CreateMap()
    {
        Tiles = new Dictionary<Node, Tile>();
        WayTiles = new Dictionary<Node, Tile>();
        WayPoints = new List<GameObject>();
        
        MapWidth = map[0].ToCharArray().Length;
        MapHeight = map.Length;
        
        for (int y = 0; y < MapHeight; y++)
        {
            char[] rowOfTiles = map[y].ToCharArray();
            
            for (int x = 0; x < MapWidth; x++)
            {
                PlaceTile(rowOfTiles[x].ToString(), x, y);
            }
        }
    }
    
    private void PlaceTile(string index, int x, int y)
    {
        int intTileIndex;
        string stringTileIndex = null;
                      
        bool isIntIndex = int.TryParse(index, out intTileIndex);
        
        if (!isIntIndex)
        {
            stringTileIndex = index;
        }
        
        Node currentTileNode = new Node(x, y);
        Vector3 currentTilePosition = new Vector3(x * TileSize, y * TileSize, 0);
        Tile newTile = CreateSetTile(intTileIndex, stringTileIndex, currentTileNode, currentTilePosition);
        
        Tiles.Add(currentTileNode, newTile);
    }

    private Tile CreateSetTile(int intTileIndex, string stringTileIndex, Node currentTileNode, Vector3 currentTilePosition)
    {
        Tile newTile;

        if (intTileIndex == 1 || stringTileIndex == "s" || stringTileIndex == "f")
        {
            newTile = Instantiate(tilePrefabs[1]).GetComponent<Tile>();
            newTile.SetTile(currentTileNode, currentTilePosition, true);
            
            if (stringTileIndex == "s")
            {
                StartWayTile = newTile;
            }
            
            else if (stringTileIndex == "f")
            {
                FinalWayTile = newTile;
            }

            wayTilesCount++;

            return newTile;
        }
        
        else
        {
            newTile = Instantiate(tilePrefabs[intTileIndex]).GetComponent<Tile>();
            newTile.SetTile(currentTileNode, currentTilePosition);

            return newTile;
        }
    }
    
    private void CreateWayTilesList()
    {
        bool isWayListReady = false;
        bool isExistWayTile;

        Tile currentTile = StartWayTile;
        Tile nextTile;

        for (int i = 0; i < wayTilesCount; i++)
        {
            if (!isWayListReady)
            {
                CheckNeighborWayTile(currentTile, out isExistWayTile, out nextTile);

                if (isExistWayTile)
                {               
                    if (nextTile == null)
                        isWayListReady = true;
                
                    else if (WayTiles.Count == 0)
                    {
                        WayTiles.Add(currentTile.Node, currentTile);
                        currentTile.IsWayTile = false;
                        currentTile = nextTile;
                    }
                
                    else if (nextTile.Node == FinalWayTile.Node)
                    {
                        WayTiles.Add(currentTile.Node, currentTile);
                        WayTiles.Add(nextTile.Node, nextTile);
                        currentTile.IsWayTile = false;
                        nextTile.IsWayTile = false;
                        isWayListReady = true;
                    }
                
                    else if (nextTile != null)
                    {
                        if (!WayTiles.ContainsKey(currentTile.Node))
                        {
                            WayTiles.Add(currentTile.Node, currentTile);
                            currentTile = nextTile;
                        }
                    }
                }
            
                else
                {
                    isWayListReady = true;
                }
            }
        }
    }

    private void CheckNeighborWayTile(Tile currentTile, out bool isExistWayTile, out Tile nextTile)
    {
        int curX = currentTile.Node.x;
        int curY = currentTile.Node.y;

        Tile tileXUup = GetTileByNode(curX, curY + 1);
        Tile tileXupY = GetTileByNode(curX + 1, curY);
        Tile tileXYdown = GetTileByNode(curX, curY - 1);
        Tile tileXdownY = GetTileByNode(curX - 1, curY);

        if (tileXUup != null && tileXUup.IsWayTile)
        {
            nextTile = tileXUup;
            nextTile.IsWayTile = false;
            isExistWayTile = true;
        }
            
        else if (tileXupY != null && tileXupY.IsWayTile)
        {
            nextTile = tileXupY;
            nextTile.IsWayTile = false;
            isExistWayTile = true;
        }
        
        else if (tileXYdown != null && tileXYdown.IsWayTile)
        {
            nextTile = tileXYdown;
            nextTile.IsWayTile = false;
            isExistWayTile = true;
        }
            
        else if (tileXdownY != null && tileXdownY.IsWayTile)
        {
            nextTile = tileXdownY;
            nextTile.IsWayTile = false;
            isExistWayTile = true;
        }
        
        else
        {
            nextTile = null;
            isExistWayTile = false;
        } 
    }
    
    private Tile GetTileByNode(int x, int y)
    {
        Tile tileByNode;
        
        if (Tiles.TryGetValue(new Node(x, y), out tileByNode))
            return tileByNode;

        return null;
    }

    private void CreateWaypoints()
    {
        GameObject wayPoint;

        int previousIndex = 0;
        int currentIndex = 1;
        int nextIndex = 2;

        Node previousNode, currentNode, nextNode;
        
        foreach (KeyValuePair<Node, Tile> tile in WayTiles)
        {
            previousNode = WayTiles.ElementAt(previousIndex).Key;
            currentNode = WayTiles.ElementAt(currentIndex).Key;
            nextNode = WayTiles.ElementAt(nextIndex).Key;          
            
            if (tile.Key == StartWayTile.Node)
            {
                wayPoint = CreateWaypoint(tile.Value);
                WayPoints.Add(wayPoint);
            }
            
            else if (tile.Key == FinalWayTile.Node)
            {
                wayPoint = CreateWaypoint(WayTiles.ElementAt(nextIndex).Value);
                WayPoints.Add(wayPoint);
            }
            
            else
            {
                if (CheckOnCornerTile(previousNode, currentNode, nextNode))
                {
                    wayPoint = CreateWaypoint(WayTiles.ElementAt(currentIndex).Value);
                    WayPoints.Add(wayPoint);
                }

                if (nextIndex != wayTilesCount - 1)
                {
                    previousIndex++;
                    currentIndex++;
                    nextIndex++;
                }
            }
        }
    }

    private GameObject CreateWaypoint(Tile tile)
    {
        GameObject gObj = new GameObject();
        gObj.transform.position = tile.transform.position;
        gObj.transform.SetParent(GameObject.Find("WayPoints").transform);

        return gObj;
    }

    private bool CheckOnCornerTile(Node previousNode, Node currentNode, Node nextNode)
    {
        if (previousNode.y == currentNode.y && (nextNode.y > currentNode.y || nextNode.y < currentNode.y))
            return true;
        
        else if (previousNode.x == currentNode.x && (nextNode.x > currentNode.x || nextNode.x < currentNode.x))
            return true;
        
        return false;
    }
    
    private void PlaceCamera()
    {
        Transform cameraPosition = GetTileByNode(MapWidth / 2, MapHeight / 2).GetComponent<Transform>();

        Camera.main.transform.position = new Vector3(cameraPosition.position.x , cameraPosition.position.y , -10);
        
    }

    private void CheckWayTilesList()
    {
        foreach (KeyValuePair<Node, Tile> oneTile in WayTiles)
        {
            Debug.Log(oneTile.Key.x + "|" + oneTile.Key.y); 
            oneTile.Value.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}

