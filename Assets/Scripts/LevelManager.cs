using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelsGrid;
    [SerializeField] private GameObject levelButtonPref;
    
    void Start()
    {
        CreateLevelGrid();
    }

    private void CreateLevelGrid()
    {
        Dictionary<string, LevelData> levelsData = SaveLoadManager.LoadLevelsData();

        foreach (KeyValuePair<string, LevelData> level in levelsData)
        {
            GameObject levelButton = Instantiate(levelButtonPref, transform.position, Quaternion.identity);
            
            levelButton.name = "Level_" + level.Value.Level;
            levelButton.transform.Find("LevelText").GetComponent<Text>().text = "Level " + level.Value.Level;
            levelButton.GetComponent<Button>().onClick.AddListener(delegate { LoadScene(levelButton.name); });
            
            levelButton.transform.SetParent(levelsGrid.transform, false);
        }
    }

    public void LoadScene(string buttonName)
    {
        SceneManager.LoadScene(buttonName);
    }
}
