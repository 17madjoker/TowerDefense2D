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
        bool previousLevelComplete = true;

        foreach (KeyValuePair<string, LevelData> level in levelsData)
        {
            GameObject levelButton = Instantiate(levelButtonPref, transform.position, Quaternion.identity);
            
            levelButton.name = "Level_" + level.Value.Level;
            
            levelButton.transform.Find("LevelText").GetComponent<Text>().text = "Level " + level.Value.Level;

            foreach (KeyValuePair<LevelData.starCondition, bool> star in level.Value.LevelStars)
            {
                if (star.Value)
                    levelButton.transform.Find("StarText").GetComponent<Text>().text += "<color=#FFA726>★</color>";

                else
                    levelButton.transform.Find("StarText").GetComponent<Text>().text += "★";
            }
            
            levelButton.GetComponent<Button>().onClick.AddListener(delegate { LoadScene(levelButton.name); });

            if (!previousLevelComplete)
                levelButton.GetComponent<Button>().interactable = false;
            
            levelButton.transform.SetParent(levelsGrid.transform, false);

            previousLevelComplete = level.Value.IsComplete;
        }
    }

    public void LoadScene(string buttonName)
    {
        SceneManager.LoadScene(buttonName);
    }
}
